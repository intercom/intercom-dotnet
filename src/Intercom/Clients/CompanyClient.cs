using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intercom.Core;
using Intercom.Data;
using Newtonsoft.Json;

namespace Intercom.Clients
{
    // TODO: List companies by Tag or Segment
    public class CompanyClient : Client
    {
        private const String COMPANIES_RESOURCE = "companies";

        public CompanyClient(Authentication authentication)
            : base(INTERCOM_API_BASE_URL, COMPANIES_RESOURCE, authentication)
        {
        }

        public CompanyClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, COMPANIES_RESOURCE, authentication)
        {
        }

        public Company Create(Company company)
        {
            return CreateOrUpdate(company);
        }

        public Company Update(Company company)
        {
            return CreateOrUpdate(company);
        }

        private Company CreateOrUpdate(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            if (company.custom_attributes != null && company.custom_attributes.Any())
            {
                if (company.custom_attributes.Count > 100)
                    throw new ArgumentException("Maximum of 100 fields.");

                foreach (var attr in company.custom_attributes)
                {
                    if (attr.Key.Contains(".") || attr.Key.Contains("$"))
                        throw new ArgumentException(String.Format("Field names must not contain Periods (.) or Dollar ($) characters. key: {0}", attr.Key));

                    if (attr.Key.Length > 190)
                        throw new ArgumentException(String.Format("Field names must be no longer than 190 characters. key: {0}", attr.Key));

                    if (attr.Value == null)
                        throw new ArgumentException(String.Format("'value' is null. key: {0}", attr.Key));
                }
            }

            ClientResponse<Company> result = null;
            result = Post<Company>(Transform(company));
            return result.Result;
        }

        public Company View(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
            ClientResponse<Company> result = null;
            result = Get<Company>(resource: COMPANIES_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;
        }

        public Company View(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<Company> result = null;

            if (!String.IsNullOrEmpty(company.id))
            {
                result = Get<Company>(resource: COMPANIES_RESOURCE + Path.DirectorySeparatorChar + company.id);
            }
            else if (!String.IsNullOrEmpty(company.name))
            {
                parameters.Add(Constants.NAME, company.name);
                result = Get<Company>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(company.company_id))
            {
                parameters.Add(Constants.COMPANY_ID, company.company_id);
                result = Get<Company>(parameters: parameters);
            }
            else
            {
                throw new ArgumentException("you need to provide either 'company.id', 'company.company_id' to view a company.");
            }
            return result.Result;
        }

        public Companies List()
        {
            ClientResponse<Companies> result = null;
            result = Get<Companies>();
            return result.Result;
        }

        public Companies List(Dictionary<String, String> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (!parameters.Any())
            {
                throw new ArgumentException("'parameters' argument is empty.");
            }

            ClientResponse<Companies> result = null;
            result = Get<Companies>(parameters: parameters);
            return result.Result;
        }

        public Companies Scroll(String scrollParam = null)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            ClientResponse<Companies> result = null;

            if (!String.IsNullOrWhiteSpace(scrollParam))
            {
                parameters.Add("scroll_param", scrollParam);
            }

            result = Get<Companies>(parameters: parameters, resource: COMPANIES_RESOURCE + Path.DirectorySeparatorChar + "scroll");
            return result.Result;
        }

        public Users ListUsers(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<Users> result = null;

            if (!String.IsNullOrEmpty(company.id))
            {
                String resource = company.id + Path.DirectorySeparatorChar + "users";
                result = Get<Users>(resource: COMPANIES_RESOURCE + Path.DirectorySeparatorChar + resource);
            }
            else if (!String.IsNullOrEmpty(company.company_id))
            {
                parameters.Add(Constants.TYPE, Constants.USER);
                parameters.Add(Constants.COMPANY_ID, company.company_id);
                result = Get<Users>(parameters: parameters);
            }
            else
            {
                throw new ArgumentException("you need to provide either 'company.id', 'company.company_id' to list users of a company.");
            }

            return result.Result;
        }

        public Users ListUsers(String companyId)
        {
            if (String.IsNullOrEmpty(companyId))
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            String resource = companyId + Path.DirectorySeparatorChar + "users";
            ClientResponse<Users> result = null;
            result = Get<Users>(resource: COMPANIES_RESOURCE + Path.DirectorySeparatorChar + resource);
            return result.Result;
        }

        private String Transform(Company company)
        {
            var body = new
            {
                remote_created_at = company.remote_created_at,
                company_id = company.company_id,
                name = company.name,
                monthly_spend = company.monthly_spend,
                custom_attributes = company.custom_attributes,
                plan = company.plan != null ? company.plan.name : null,
                website = company.website,
                size = company.size,
                industry = company.industry
            };

            return JsonConvert.SerializeObject(body,
                           Formatting.None,
                           new JsonSerializerSettings
                           {
                               NullValueHandling = NullValueHandling.Ignore
                           });
        }
    }
}
