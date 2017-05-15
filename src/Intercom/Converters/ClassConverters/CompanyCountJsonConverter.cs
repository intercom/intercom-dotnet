﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Intercom.Clients;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Intercom.Converters.ClassConverters
{
    public class CompanyCountJsonConverter: JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, 
                                        Type objectType, 
                                        object existingValue,
                                        JsonSerializer serializer)
        {
            JObject j = null;

            try
            {
                j = JObject.Load(reader);

                if (objectType == typeof(CompanyTagCount))
                {
                    Dictionary<String, int> count = GetCompanyTagOrSegmentCount(j, j["company"]["tag"] as JArray);
                    return new CompanyTagCount() { tags = count };
                }
                else if (objectType == typeof(CompanySegmentCount))
                {
                    Dictionary<String, int> count = GetCompanyTagOrSegmentCount(j, j["company"]["segment"] as JArray);
                    return new CompanySegmentCount() { segments = count };
                }
                else
                {
                    List<UserCount.UserCountEntry> count = GetCompanyUserCount(j, j["company"]["user"] as JArray);
                    return new CompanyUserCount() { counts = count };
                }
            }
            catch (Exception ex)
            {
                throw new JsonConverterException("Error while serializing CompanyCount endpoint json result.", ex)
                { 
                    Json = j == null ? String.Empty : j.ToString(),
                    SerializationType = objectType.FullName
                };
            }
        }

        public override void WriteJson(JsonWriter writer, 
                                       object value,
                                       JsonSerializer serializer)
        {
            String s = JsonConvert.SerializeObject(value,
                           Formatting.None,
                           new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            writer.WriteRawValue(s);
        }

        private Dictionary<String, int> GetCompanyTagOrSegmentCount(JObject j, JArray result)
        {
            Dictionary<String, int> count = new Dictionary<String, int>();

            foreach (var r in result)
            {
                JProperty c = r.First as JProperty;

                if (c != null)
                {
                    int value = 0;
                    int.TryParse(c.Value.ToString(), out value);
                    count.Add(c.Name, value);
                }
            }

            return count;
        }

        private List<UserCount.UserCountEntry> GetCompanyUserCount(JObject j, JArray result)
        {
            List<UserCount.UserCountEntry> count = new List<UserCount.UserCountEntry>();

            foreach (var r in result)
            {
                if (r != null)
                {
                    int value = 0;
                    String name = String.Empty;
                    String remoteCompanyId = String.Empty;
          
                    remoteCompanyId = r[Constants.REMOTE_COMPANY_ID].Value<String>();

                    JProperty prop = r.First as JProperty;
                    name = prop.Name;

                    int.TryParse(prop.Value.ToString(), out value);

                    count.Add(
                        new UserCount.UserCountEntry()
                        {
                            count = value,
                            name = name,
                            remote_company_id = remoteCompanyId
                        });
                }
            }

            return count;
        }

        public override bool CanRead
        {
            get { return true; }
        }
    }
}