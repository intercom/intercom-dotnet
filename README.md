![Build Status](https://www.myget.org/BuildSource/Badge/intercom-dotnet?identifier=9009384b-3f0e-4d23-aa53-3bac173f29bb)

# intercom-dotnet

.NET bindings for the [Intercom API](https://developers.intercom.io/reference)

 - [Installation](#installation)
 - [Resources](#resources)
 - [Authorization](#authorization)
 - [Usage](#usage)
 - [Idioms](#idioms)
 - [Roadmap](#roadmap)

## Add a dependency

### nuget

Run the nuget command for installing the client as `Install-Package Intercom.Dotnet.Client`

## Resources

Resources this API supports:

- [Users](#users)
- [Contacts](#contacts)
- [Companies](#companies)
- [Admins](#admins)
- [Events](#events)
- [Tags](#tags)
- [Segments](#segments)
- [Notes](#notes)
- [Conversations](#conversations)
- [Counts](#counts)
- [Webhooks](#webhooks)

Each of these resources is represented through the dontnet client by a Class as `ResourceClient`.

**E.g.**: for **users**, you can use the `UsersClient`. For **segments**, you can use `SegmentsClient`.


## Authorization

You can set the app's id and api key via creating an `Authentication` object and passing it to the suitable `Client` class:

```cs
UsersClient countsClient = new UsersClient(new Authentication("AppId", "AppKey"));
```


## Usage

### Users


```cs
// Create UsersClient instance
UsersClient usersClient = new UsersClient(new Authentication("AppId", "AppKey"));

// Create a user
User user = usersClient.Create(new User() { user_id = "my_id", name = "first last" });

// View a user (by id, user_id or email)
User user = usersClient.View("100300231");
User user = usersClient.View(new User() { email = "example@example.com" });
User user = usersClient.View(new User() { id = "100300231" });
User user = usersClient.View(new User() { user_id = "my_id" });

// List users and iterating through users
Users users = usersClient.List();

foreach(User u in users.users)
    Console.WriteLine(u.email);

// Update a user with a new company (with user assigned company_id)
User user = usersClient.Update(new User() { 
                                email = "example@example.com", 
                                companies = new List<Company>() { 
                                        new Company() { company_id = "new_company" } } });

// Delete a user
usersClient.Delete("100300231"); // with intercom generated user's id
usersClient.Delete(new User() { email = "example@example.com" });
usersClient.Delete(new User() { user_id = "my_id" });

// Update User's LastSeenAt (multiple ways for updating)
User user = usersClient.UpdateLastSeenAt("100300231");
User user = usersClient.UpdateLastSeenAt(new User() { id = "100300231" });
User user = usersClient.UpdateLastSeenAt("100300231", 1462110718);
User user = usersClient.UpdateLastSeenAt(new User() { id = "100300231" }, 1462110718);

// Increment User's Session
usersClient.IncrementUserSession(new User() { id = "100300231" });

// Removing User's companies
User user = usersClient.RemoveCompanyFromUser("100300231", new List<String>() { "true_company" });

```

### Contacts

```cs
// Create ContactsClient instance
ContactsClient contactsClient = new ContactsClient(new Authentication("AppId", "AppKey"));

// Create a contact
Contact contact = contactsClient.Create(new Contact() { });
Contact contact = contactsClient.Create(new Contact() { name = "lead_name" });

// View a contact (by id, or user_id)
Contact contact = contactsClient.View("100300231");
Contact contact = contactsClient.View(new Contact() { id = "100300231" });
Contact contact = contactsClient.View(new Contact() { user_id = "my_lead_id" });

// Update a contact (by id, or user_id)
Contact contact = contactsClient.Update(
                    new Contact()
                    {   
                        email = "example@example", 
                        companies = new List<Company>() { new Company() { company_id = "new_company" } }
                    });

// List users and iterating through users
Contacts contacts = contactsClient.List();

foreach (Contact c in contacts.contacts)
    Console.WriteLine(c.email);

// Delete a contact
contactsClient.Delete("100300231");
contactsClient.Delete(new Contact() { id = "100300231" });
contactsClient.Delete(new Contact() { user_id = "my_id" });
```

### Companies

```cs
// Create ContactsClient instance
CompanyClient companyClient = new CompanyClient(new Authentication("AppId", "AppKey"));

// Create a contact
Company company = companyClient.Create(new Company());
Company company = companyClient.Create(new Company() { name = "company_name" });

// View a contact (by id, or user_id)
Company company = companyClient.View("100300231");
Company company = companyClient.View(new Company() { id = "100300231" });
Company company = companyClient.View(new Company() { company_id = "my_company_id" });
Company company = companyClient.View(new Company() { name = "my_company_name" });

// Update a contact (by id, or user_id)
Company company = companyClient.Update(
                    new Company()
                    {   
                        company_id = "example@example", 
                        monthly_spend = 100
                    });

// List companies and iterating through
Companies companies = companyClient.List();

foreach (Company c in companies.companies)
    Console.WriteLine(c.name);

// List a Company's registered users
Users users = companyClient.ListUsers(new Company() { id = "100300231" });
Users users = companyClient.ListUsers(new Company() { company_id = "my_company_id" });
```

### Admins

```cs
// Create ContactsClient instance
AdminsClient adminsClient = new AdminsClient(new Authentication("AppId", "AppKey"));

// View an admin (by id)
Admin admin = adminsClient.View("100300231");
Admin admin = adminsClient.View(new Admin() { id = "100300231" });

// List companies and iterating through
Admins admins = adminsClient.List();

foreach (Admin admin in admins.admins)
    Console.WriteLine(admin.name);
```

### Segments

```cs
// Create ContactsClient instance
SegmentsClient segmentsClient = new SegmentsClient(new Authentication("AppId", "AppKey"));

// View a segment (by id)
Segment segment = segmentsClient.View("100300231");
Segment segment = segmentsClient.View(new Segment() { id = "100300231" });

// List companies and iterating through
Segments segments = segmentsClient.List();

foreach (Segment segment in segments.segments)
    Console.WriteLine(segment.name);
```

### Notes

```cs
// Create NotesClient instance
NotesClient notesClient = new NotesClient(new Authentication("AppId", "AppKey"));

// Create a note (by User, body and admin_id)
Note note = notesClient.Create(
    new Note() { 
    author = new Author() { id = "100300231_admin_id" },
    user =  new User() { email = "example@example.com" },
    body = "this is a new note"
});

Note note = notesClient.Create(new User() { email = "example@example.com" }, "this is a new note", "100300231_admin_id");

// View a note
Note note = notesClient.View("2001");

// List User's notes
Notes notes = notesClient.List(new User() { id = "100300231"});

foreach (Note n in notes.notes)
    Console.WriteLine(n.user.name);
```

### Counts

```cs
// Create NotesClient instance
CountsClient countsClient = new CountsClient(new Authentication("AppId", "AppKey"));

// Get AppCount
AppCount appCount = countsClient.GetAppCount();

// Get ConversationAppCount
ConversationAppCount conversationAppCount = countsClient.GetConversationAppCount();

// Get ConversationAdminCount
ConversationAdminCount conversationAdminCount = countsClient.GetConversationAdminCount();

// Get CompanySegmentCount
CompanySegmentCount companySegmentCount = countsClient.GetCompanySegmentCount();

// Get CompanyTagCount
CompanyTagCount companyTagCount = countsClient.GetCompanyTagCount();

// Get CompanyUserCount
CompanyUserCount companyUserCount = countsClient.GetCompanyUserCount();

// Get UserSegmentCount
UserSegmentCount userSegmentCount = countsClient.GetUserSegmentCount();

// Get UserTagCount
UserTagCount userTagCount = countsClient.GetUserTagCount();
```

### Tags

```cs
// Create UsersClient instance
TagsClient tagsClient = new TagsClient(new Authentication("AppId", "AppKey"));

// Create a tag
Tag tag = tagsClient.Create(new Tag() { name = "new_tag" });

// List tags and iterate through
Tags tags = tagsClient.List();

foreach(Tag t in tags.tags)
    Console.WriteLine(t.name);

// Delete a tag
tagsClient.Delete(new Tag() { id = "100300231" });


// Tag User, Company or Contact (Lead)
tagsClient.Tag("new_tag", new List<Company>() { new Company(){ id = "1000_company_id" } });
tagsClient.Tag("new_tag", new List<Contact>() { new Company(){ id = "1000_contact_id" } });
tagsClient.Tag("new_tag", new List<User>() { new Company(){ id = "1000_user_id" } });
tagsClient.Tag("new_tag", new List<String>() {"1000_company_id" ,"1001_company_id" }, TagsClient.EntityType.Company);


// Untag User, Company or Contact (Lead)
tagsClient.Untag("new_tag", new List<Company>() { new Company(){ id = "1000_company_id" } });
tagsClient.Untag("new_tag", new List<Contact>() { new Company(){ id = "1000_contact_id" } });
tagsClient.Untag("new_tag", new List<User>() { new Company(){ id = "1000_user_id" } });
tagsClient.Untag("new_tag", new List<String>() {"1000_company_id" ,"1001_company_id" }, TagsClient.EntityType.Company);

```

### Events

```cs
// Create UsersClient instance
EventsClient eventsClient = new EventsClient(new Authentication("AppId", "AppKey"));

// Create an event
Event event = eventsClient.Create(new Event() { event_name = "new_event", created_at = 1462110718  });

// Create a tag with Metadata (Simple, MonetaryAmounts and RichLinks)
Metadata metadata = new Metadata();
metadata.Add("simple", 100);
metadata.Add("simple_1", "two");
metadata.Add("money", new Metadata.MonetaryAmount(100, "eur"));
metadata.Add("richlink", new Metadata.RichLink("www.example.com", "value1"));

Event event = eventsClient.Create(new Event() { event_name = "new_event", created_at = 1462110718, metadata = metadata  });

// List tags and iterate through
Events events = eventsClient.List();

foreach(Event ev in events.event)
    Console.WriteLine(ev.event_name);
```

### Conversations

```cs
// Create ConversationsClient instance
ConversationsClient conversationsClient = new ConversationsClient(new Authentication("AppId", "AppKey"));

// View any type of conversation
conversationsClient.View("100300231");
conversationsClient.View("100300231", displayAsPlainText: true);

// Create AdminConversationsClient instance
AdminConversationsClient adminConversationsClient = new AdminConversationsClient(new Authentication("AppId", "AppKey"));

// Create Admin initiated Conversation
AdminConversationMessage admin_message = 
    adminConversationsClient.Create(new AdminConversationMessage(
            from: new AdminConversationMessage.From("1000_admin_id"),
            to: new AdminConversationMessage.To(id: "1000_user_id"),
            message_type: AdminConversationMessage.MessageType.EMAIL,
            template: AdminConversationMessage.MessageTemplate.PERSONAL,
            subject: "this is a subject",
            body: "this is an email body"));


// Create Admin initiated Conversation's reply
AdminConversationReply admin_reply = 
    adminConversationsClient.Reply(
        new AdminConversationReply(
            conversationId: "1000_conversation_id", 
            adminId: "1000_admin_id",
            messageType: AdminConversationReply.ReplyMessageType.COMMENT,
            body: "this is a reply body"));


// Create UserConversationsClient instance
UserConversationsClient userConversationsClient = new UserConversationsClient(new Authentication("AppId", "AppKey"));

// Create User initiated Conversation
UserConversationMessage user_message = 
    userConversationsClient.Create(
        new UserConversationMessage(
            from: new UserConversationMessage.From(id: "1000_user_id"),
            body: "this is a user's message body"));

// Create User initiated Conversation's reply
UserConversationReply user_reply = 
    userConversationsClient.Reply(
        new UserConversationReply(
            conversationId: "1000_conversation_id",
            body: "this is a user's reply body",
            attachementUrls: new List<String>() { "www.example.com/example.png", "www.example.com/example.txt" }));
```

### Webhooks

Not supported.

### Bulk APIs

Not supported.

## Idioms

### Exceptions

To be written.

### Pagination

To be written.

## Roadmap

- Functions Comments (for IntelliSense support)
- Support Pagination
- Support Bulk Apis
- Support Webhooks
- Support Async
- More Integration tests
- Remove RestSharp dependency