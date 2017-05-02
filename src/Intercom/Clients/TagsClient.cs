using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using Newtonsoft.Json;

namespace Intercom.Clients
{
	public class TagsClient : Client
	{
		public enum EntityType
		{
			User,
			Contact,
			Company
		}

		private const String TAGS_RESOURCE = "tags";

		public TagsClient(Authentication authentication)
			: base(INTERCOM_API_BASE_URL, TAGS_RESOURCE, authentication)
		{
		}

		public TagsClient(String intercomApiUrl, Authentication authentication)
			: base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, TAGS_RESOURCE, authentication)
		{
		}

		public async Task<Tag> Create(Tag tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException(nameof(tag));
			}

			if (string.IsNullOrEmpty(tag.name))
			{
				throw new ArgumentException("you need to provide 'tag.name' to create a tag.");
			}

			ClientResponse<Tag> result = null;
			result = await Post<Tag>(tag);
			return result.Result;
		}

		public async Task<Tag> Update(Tag tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException(nameof(tag));
			}

			if (String.IsNullOrEmpty(tag.name) || String.IsNullOrEmpty(tag.id))
			{
				throw new ArgumentException("you need to provide 'tag.id', 'tag.name' to update a tag.");
			}

			ClientResponse<Tag> result = null;
			result = await Post<Tag>(tag);
			return result.Result;
		}

		public async Task<Tag> View(String id)
		{
			if (String.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException(nameof(id));
			}

			ClientResponse<Tag> result = null;
			result = await Get<Tag>(resource: TAGS_RESOURCE + Path.DirectorySeparatorChar + id);
			return result.Result;
		}

		public async Task<Tag> View(Tag tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException(nameof(tag));
			}

			ClientResponse<Tag> result = null;

			if (!String.IsNullOrEmpty(tag.id))
			{
				result = await Get<Tag>(resource: TAGS_RESOURCE + Path.DirectorySeparatorChar + tag.id);
			}
			else
			{
				throw new ArgumentException("you need to provide 'tag.id' to view a tag.");
			}

			return result.Result;
		}

		public async Task<Tags> List()
		{
			ClientResponse<Tags> result = null;
			result = await Get<Tags>();
			return result.Result;
		}

		public async Task<Tags> List(Dictionary<String, String> parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException(nameof(parameters));
			}

			if (!parameters.Any())
			{
				throw new ArgumentException("'parameters' argument is empty.");
			}

			ClientResponse<Tags> result = null;
			result = await Get<Tags>(parameters: parameters);
			return result.Result;
		}

		public async Task Delete(Tag tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException(nameof(tag));
			}

			if (String.IsNullOrEmpty(tag.id))
			{
				throw new ArgumentException("you need to provide 'tag.id' to delete a tag.");
			}

			await Delete<Tag>(resource: TAGS_RESOURCE + Path.DirectorySeparatorChar + tag.id);
		}

		public async Task Delete(String id)
		{
			if (String.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException(nameof(id));
			}

			await Delete<Tag>(resource: TAGS_RESOURCE + Path.DirectorySeparatorChar + id);
		}

		public async Task<Tag> Tag(String name, List<Company> companies)
		{
			if (String.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException(nameof(name));
			}

			if (companies == null)
			{
				throw new ArgumentNullException(nameof(companies));
			}

			if (!companies.Any())
			{
				throw new ArgumentException("'companies' argument should include more than one company.");
			}

			foreach (var c in companies)
				if (String.IsNullOrEmpty(c.id) && String.IsNullOrEmpty(c.company_id))
					throw new ArgumentException("you need to provide either 'company.id', 'company.company_id' to tag a company.");

			ClientResponse<Tag> result = null;

			var tags = new { name = name, companies = companies.Select(c => new { id = c.id }) };
			String body = SerializeTag(tags);
			result = await Post<Tag>(body);

			return result.Result;
		}

		public async Task<Tag> Tag(String name, List<User> users)
		{
			if (String.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException(nameof(name));
			}

			if (users == null)
			{
				throw new ArgumentNullException(nameof(users));
			}

			if (!users.Any())
			{
				throw new ArgumentException("'users' argument should include more than one user.");
			}

			ClientResponse<Tag> result = null;
			String body = CreateBody(name, false, users: users);
			result = await Post<Tag>(body);

			return result.Result;
		}

		public async Task<Tag> Tag(String name, List<Contact> contacts)
		{
			if (String.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException(nameof(name));
			}

			if (contacts == null)
			{
				throw new ArgumentNullException(nameof(contacts));
			}

			if (!contacts.Any())
			{
				throw new ArgumentException("'users' argument should include more than one user.");
			}

			ClientResponse<Tag> result = null;
			String body = CreateBody(name, false, contacts.ToList<User>());
			result = await Post<Tag>(body);

			return result.Result;
		}

		public async Task<Tag> Tag(String name, List<String> ids, EntityType tagType)
		{
			switch (tagType)
			{
				case EntityType.Company:
					return await Tag(name, ids.Select(id => new Company() { id = id }).ToList());
				case EntityType.Contact:
					return await Tag(name, ids.Select(id => new Contact() { id = id }).ToList());
				case EntityType.User:
					return await Tag(name, ids.Select(id => new User() { id = id }).ToList());
				default:
					return await Tag(name, ids.Select(id => new User() { id = id }).ToList());
			}
		}

		public async Task<Tag> Untag(String name, List<User> users)
		{
			if (String.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException(nameof(name));
			}

			if (users == null)
			{
				throw new ArgumentNullException(nameof(users));
			}

			if (!users.Any())
			{
				throw new ArgumentException("'users' argument should include more than one user.");
			}

			ClientResponse<Tag> result = null;
			String body = CreateBody(name, true, users: users);
			result = await Post<Tag>(body);

			return result.Result;
		}

		public async Task<Tag> Untag(String name, List<Company> companies)
		{
			if (String.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException(nameof(name));
			}

			if (companies == null)
			{
				throw new ArgumentNullException(nameof(companies));
			}

			if (!companies.Any())
			{
				throw new ArgumentException("'companies' argument should include more than one company.");
			}

			foreach (var c in companies)
				if (String.IsNullOrEmpty(c.id) && String.IsNullOrEmpty(c.company_id))
					throw new ArgumentException("you need to provide either 'company.id', 'company.company_id' to tag a company.");

			ClientResponse<Tag> result = null;

			var tags = new { name = name, companies = companies.Select(c => new { id = c.id, untag = true }) };
			String body = SerializeTag(tags);
			result = await Post<Tag>(body);

			return result.Result;
		}

		public async Task<Tag> Untag(String name, List<Contact> contacts)
		{

			if (String.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException(nameof(name));
			}

			if (contacts == null)
			{
				throw new ArgumentNullException(nameof(contacts));
			}

			if (!contacts.Any())
			{
				throw new ArgumentException("'users' argument should include more than one user.");
			}


			ClientResponse<Tag> result = null;
			String body = CreateBody(name, true, contacts.ToList<User>());
			result = await Post<Tag>(body);

			return result.Result;
		}

		public async Task<Tag> Untag(String name, List<String> ids, EntityType tagType)
		{
			switch (tagType)
			{
				case EntityType.Company:
					return await Untag(name, ids.Select(id => new Company() { id = id }).ToList());
				case EntityType.Contact:
					return await Untag(name, ids.Select(id => new Contact() { id = id }).ToList());
				case EntityType.User:
					return await Untag(name, ids.Select(id => new User() { id = id }).ToList());
				default:
					return await Untag(name, ids.Select(id => new User() { id = id }).ToList());
			}
		}

		private String CreateBody(String name, bool untag, List<User> users)
		{
			foreach (var u in users)
				if (String.IsNullOrEmpty(u.id) && String.IsNullOrEmpty(u.user_id) && string.IsNullOrEmpty(u.email))
					throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to tag a user.");

			object tags = BuildTag(name, untag, users);
			return SerializeTag(tags);
		}

		private object BuildTag(String name, bool untag, List<User> users)
		{
			return new { name = name, users = users.Select(u => new { id = u.id, user_id = u.user_id, email = u.email, untag = untag }) };
		}

		private String SerializeTag(object tag)
		{
			return JsonConvert.SerializeObject(tag,
				Formatting.None,
				new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				});
		}
	}
}