using System;
using Library.Core;
using Library.Data;
using Library.Clients;
using Library.Exceptions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using RestSharp;
using RestSharp.Authenticators;
using System.IO;

namespace Library.Clients
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
            ClientResponse<Company> result = null;
            CreateOrUpdate(company);
            return result.Result;
        }

        public Company Update(Company company)
        {
            ClientResponse<Company> result = null;
            CreateOrUpdate(company);
            return result.Result;
        }

        private Company CreateOrUpdate(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException("'company' argument is null.");
            }

            if (company.custom_attributes != null && company.custom_attributes.Any())
            {
                if (company.custom_attributes.Count > 100)
                    throw new ArgumentException("Maximum of 100 fields.");

                foreach (var attr in company.custom_attributes)
                {
                    String company_id = String.IsNullOrEmpty(company.company_id) ? String.Empty : company.company_id;

                    if (attr.Key.Contains(".") || attr.Key.Contains("$"))
                        throw new ArgumentException(String.Format("Field names must not contain Periods (.) or Dollar ($) characters. company_id: {0}", company_id));

                    if (attr.Key.Length > 190)
                        throw new ArgumentException(String.Format("Field names must be no longer than 190 characters. company_id: {0}", company_id));

                    if (attr.Value.Length > 255)
                        throw new ArgumentException(String.Format("String field values must be no longer than 255 characters. company_id: {0}", company_id));
                }
            }

            ClientResponse<Company> result = null;
            result = Post<Company>(company);
            return result.Result;
        }

        public Company View(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("'parameters' argument is null.");
            }

            ClientResponse<Company> result = null;
            result = Get<Company>(resource: COMPANIES_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;		
        }

        public Company View(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException("'company' argument is null.");
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<Company> result = null;

            if (!String.IsNullOrEmpty(company.id))
            {
                result = Delete<Company>(resource: COMPANIES_RESOURCE + Path.DirectorySeparatorChar + company.id);
            }
            else if (!String.IsNullOrEmpty(company.name))
            {
                parameters.Add(Constants.NAME, company.name);
                result = Delete<Company>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(company.company_id))
            {
                parameters.Add(Constants.COMPANY_ID, company.company_id);
                result = Delete<Company>(parameters: parameters);
            }
            else
            {
                throw new ArgumentNullException("you need to provide either 'company.id', 'company.company_id' to view a company.");
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
                throw new ArgumentNullException("'parameters' argument is null.");
            }

            if (!parameters.Any())
            {
                throw new ArgumentException("'parameters' argument should include company_id parameter.");
            }

            ClientResponse<Companies> result = null;
            result = Get<Companies>(parameters: parameters);
            return result.Result;
        }

        public Users ListUsers(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException("'company' argument is null.");
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
                throw new ArgumentNullException("you need to provide either 'company.id', 'company.company_id' to list users of a company.");
            }

            return result.Result;
        }

        public Users ListUsers(String companyId)
        {
            if (String.IsNullOrEmpty(companyId))
            {
                throw new ArgumentNullException("'company.id' is null or empty.");
            }

            String resource = companyId + Path.DirectorySeparatorChar + "users";
            ClientResponse<Users> result = null;
            result = Get<Users>(resource: COMPANIES_RESOURCE + Path.DirectorySeparatorChar + resource);
            return result.Result;		
        }
    }
}