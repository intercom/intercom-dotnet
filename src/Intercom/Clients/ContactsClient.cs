using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Intercom.Core;
using Intercom.Data;
using Newtonsoft.Json;

namespace Intercom.Clients
{
	public class ContactsClient : Client
	{
		public static class ContactSortBy
		{
			public const String created_at = "created_at";
			public const String updated_at = "updated_at";
			public const String signed_up_at = "signed_up_at";
		}

		private const String CONTACTS_RESOURCE = "contacts";

		public ContactsClient(Authentication authentication)
			: base(INTERCOM_API_BASE_URL, CONTACTS_RESOURCE, authentication)
		{
		}

		public ContactsClient(String intercomApiUrl, Authentication authentication)
			: base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, CONTACTS_RESOURCE, authentication)
		{
		}

		public async Task<Contact> Create(Contact contact)
		{
			if (contact == null)
			{
				throw new ArgumentNullException(nameof(contact));
			}

			ClientResponse<Contact> result = null;

			if (contact == null)
				result = await Post<Contact>(String.Empty);
			else
				result = await Post<Contact>(contact);

			return result.Result;
		}

		public async Task<Contact> Update(Contact contact)
		{
			if (contact == null)
			{
				throw new ArgumentNullException(nameof(contact));
			}

			if (String.IsNullOrEmpty(contact.id) && String.IsNullOrEmpty(contact.user_id))
			{
				throw new ArgumentException("you need to provide either 'id', 'user_id', 'email' to view a user.");
			}

			ClientResponse<Contact> result = null;
			result = await Post<Contact>(contact);
			return result.Result;
		}

		public async Task<Contact> View(String id)
		{
			if (String.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException(nameof(id));
			}

			ClientResponse<Contact> result = null;
			result = await Get<Contact>(resource: CONTACTS_RESOURCE + Path.DirectorySeparatorChar + id);
			return result.Result;
		}

		public async Task<Contact> View(Contact contact)
		{
			if (contact == null)
			{
				throw new ArgumentNullException(nameof(contact));
			}

			Dictionary<String, String> parameters = new Dictionary<string, string>();
			ClientResponse<Contact> result = null;

			if (!String.IsNullOrEmpty(contact.id))
			{
				result = await Get<Contact>(resource: CONTACTS_RESOURCE + Path.DirectorySeparatorChar + contact.id);
			}
			else if (!String.IsNullOrEmpty(contact.user_id))
			{
				parameters.Add(Constants.USER_ID, contact.user_id);
				result = await Get<Contact>(parameters: parameters);
			}
			else
			{
				throw new ArgumentException("you need to provide either 'contact.id', 'contact.user_id' to view a contact.");
			}

			return result.Result;
		}

		public async Task<Contacts> List()
		{
			ClientResponse<Contacts> result = null;
			result = await Get<Contacts>();
			return result.Result;
		}

		public async Task<Contacts> List(String email)
		{
			if (String.IsNullOrEmpty(email))
			{
				throw new ArgumentNullException(nameof(email));
			}

			Dictionary<String, String> parameters = new Dictionary<string, string>();
			parameters.Add(Constants.EMAIL, email);

			ClientResponse<Contacts> result = null;
			result = await Get<Contacts>(parameters: parameters);
			return result.Result;
		}

		public async Task<Contacts> List(Dictionary<String, String> parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException(nameof(parameters));
			}

			if (!parameters.Any())
			{
				throw new ArgumentException("'parameters' argument is empty.");
			}

			ClientResponse<Contacts> result = null;
			result = await Get<Contacts>(parameters: parameters);
			return result.Result;
		}

		public async Task<Contacts> Scroll(String scrollParam = null)
		{
			Dictionary<String, String> parameters = new Dictionary<String, String>();
			if (!String.IsNullOrWhiteSpace(scrollParam))
			{
				parameters.Add("scroll_param", scrollParam);
			}

			ClientResponse<Contacts> result = null;
			result = await Get<Contacts>(parameters: parameters, resource: CONTACTS_RESOURCE + Path.DirectorySeparatorChar + "scroll");
			return result.Result;
		}

		public async Task<Contact> Delete(Contact contact)
		{
			if (contact == null)
			{
				throw new ArgumentNullException(nameof(contact));
			}

			Dictionary<String, String> parameters = new Dictionary<string, string>();
			ClientResponse<Contact> result = null;

			if (!String.IsNullOrEmpty(contact.id))
			{
				result = await Delete<Contact>(resource: CONTACTS_RESOURCE + Path.DirectorySeparatorChar + contact.id);
			}
			else if (!String.IsNullOrEmpty(contact.user_id))
			{
				parameters.Add(Constants.USER_ID, contact.user_id);
				result = await Delete<Contact>(parameters: parameters);
			}
			else
			{
				throw new ArgumentException("you need to provide either 'contact.id', 'contact.user_id' to delete a contact.");
			}

			return result.Result;
		}

		public async Task<Contact> Delete(String id)
		{
			if (String.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException(nameof(id));
			}

			ClientResponse<Contact> result = null;
			result = await Delete<Contact>(resource: CONTACTS_RESOURCE + Path.DirectorySeparatorChar + id);
			return result.Result;
		}

		public async Task<User> Convert(Contact contact)
		{
			if (contact == null)
				throw new ArgumentNullException("'contact' argument is null.");

			if (String.IsNullOrEmpty(contact.id) && String.IsNullOrEmpty(contact.user_id))
				throw new ArgumentException("you need to provide either 'contact.id', 'contact.user_id' to convert a lead.");

			Dictionary<String, String> contactBody = new Dictionary<String, String>();

			if (!String.IsNullOrEmpty(contact.id))
			{
				contactBody.Add("id", contact.id);
			}
			else
			{
				contactBody.Add("user_id", contact.user_id);
			}

			var body = new { contact = contactBody };

			var jsonBody = JsonConvert.SerializeObject(body,
						   Formatting.None,
						   new JsonSerializerSettings
						   {
							   NullValueHandling = NullValueHandling.Ignore
						   });

			var result = await Post<User>(jsonBody, resource: CONTACTS_RESOURCE + Path.DirectorySeparatorChar + "convert");

			return result.Result;
		}

		public async Task<User> Convert(Contact contact, User user)
		{
			if (contact == null)
				throw new ArgumentNullException("'contact' argument is null.");

			if (user == null)
				throw new ArgumentNullException("'user' argument is null.");

			if (String.IsNullOrEmpty(contact.id) && String.IsNullOrEmpty(contact.user_id))
				throw new ArgumentException("you need to provide either 'contact.id', 'contact.user_id' to convert a lead.");

			if (String.IsNullOrEmpty(user.id) && String.IsNullOrEmpty(user.user_id) && String.IsNullOrEmpty(user.email))
				throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', or 'user.email' to convert a lead.");

			Dictionary<String, String> contactBody = new Dictionary<String, String>();
			Dictionary<String, String> userBody = new Dictionary<String, String>();

			if (!String.IsNullOrEmpty(user.id))
			{
				userBody.Add("id", user.id);
			}
			else if (!String.IsNullOrEmpty(user.user_id))
			{
				userBody.Add("user_id", user.user_id);
			}
			else
			{
				userBody.Add("email", user.email);
			}

			if (!String.IsNullOrEmpty(contact.id))
			{
				contactBody.Add("id", contact.id);
			}
			else
			{
				contactBody.Add("user_id", contact.user_id);
			}

			var body = new { contact = contactBody, user = userBody };

			var jsonBody = JsonConvert.SerializeObject(body,
						   Formatting.None,
						   new JsonSerializerSettings
						   {
							   NullValueHandling = NullValueHandling.Ignore
						   });

			var result = await Post<User>(jsonBody, resource: CONTACTS_RESOURCE + Path.DirectorySeparatorChar + "convert");

			return result.Result;
		}
	}
}