using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using RestSharp;

namespace Intercom.Clients
{
    public class SegmentsClient: Client
    {
        private const String SEGMENTS_RESOURCE = "segments";

        public SegmentsClient(Authentication authentication)
            : base(INTERCOM_API_BASE_URL, SEGMENTS_RESOURCE, authentication)
        {
        }

        public SegmentsClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, SEGMENTS_RESOURCE, authentication)
        {
        }

        public Segments List(bool company = false)
        {
            ClientResponse<Segments> result = null;

            if (company)
            {
                Dictionary<String, String> parameters = new Dictionary<string, string>();
                parameters.Add(Constants.TYPE, Constants.COMPANY);
                result = Get<Segments>(parameters: parameters);
            }
            else
            {
                result = Get<Segments>();
            }

            return result.Result;
        }

        public Segments List(Dictionary<String, String> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (!parameters.Any())
            {
                throw new ArgumentException ("'parameters' argument is empty.");
            }

            ClientResponse<Segments> result = null;
            result = Get<Segments>(parameters: parameters);
            return result.Result;
        }

        public Segment View(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            ClientResponse<Segment> result = null;
            result = Get<Segment>(resource: SEGMENTS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;
        }

        public Segment View(Segment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            if (String.IsNullOrEmpty(segment.id))
            {
                throw new ArgumentException("you must provide value for 'segment.id'.");
            }

            ClientResponse<Segment> result = null;
            result = Get<Segment>(resource: SEGMENTS_RESOURCE + Path.DirectorySeparatorChar + segment.id);
            return result.Result;  
        }
    }
}