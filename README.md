![Build Status](https://www.myget.org/BuildSource/Badge/intercom-dotnet?identifier=9009384b-3f0e-4d23-aa53-3bac173f29bb)

# intercom-dotnet

.NET bindings for the [Intercom API](https://developers.intercom.io/reference)

 - [Installation](#add-a-dependency)
 - [Resources](#resources)
 - [Authorization](#authorization)
 - [Usage](#usage)
 - [Todo](#todo)
 - [Pull Requests](#pull-requests)

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

Each of these resources is represented through the dotnet client by a Class as `ResourceClient`.

**E.g.**: for **users**, you can use the `UsersClient`. For **segments**, you can use `SegmentsClient`.


## Authorization

> If you already have an access token you can find it [here](https://app.intercom.com/developers/_). If you want to create or learn more about access tokens then you can find more info [here](https://developers.intercom.io/docs/personal-access-tokens).

You can set the `Personal Access Token` via creating an `Authentication` object by invoking the single paramter constructor:

```cs
UsersClient usersClient = new UsersClient(new Authentication("MyPersonalAccessToken"));
```

If you are building a third party application you will need to implement OAuth by following the steps for [setting-up-oauth](https://developers.intercom.io/page/setting-up-oauth) for Intercom.

## Usage

### Users

**Create UsersClient instance**
```cs
UsersClient usersClient = new UsersClient(new Authentication("MyPersonalAccessToken"));
```

**Create user**
```cs
User user = usersClient.Create(new User() { user_id = "my_id", name = "first last" });
```

**View a user (by id, user_id or email)**
```cs
User user = usersClient.View("100300231");
User user = usersClient.View(new User() { email = "example@example.com" });
User user = usersClient.View(new User() { id = "100300231" });
User user = usersClient.View(new User() { user_id = "my_id" });
```

**Update a user with a company**
```cs
User user = usersClient.Update(new User() {
                               email = "example@example.com",
                               companies = new List<Company>() {
                                           new Company() { company_id = "new_company" } } });
```                 

**Update user's custom attributes**
```cs
Dictionary<string, object> customAttributes = new Dictionary<string, object>();
customAttributes.Add("total", "100.00");
customAttributes.Add("account_level", "1");

User user = usersClient.View("100300231");
user.custom_attributes = customAttributes;

user = usersClient.Update(user);
```

**List users and iterating through them**
<br>Limited to up to 10k records, if you want to list more records please use the Scroll API

```cs
Users users = usersClient.List();

foreach(User u in users.users)
    Console.WriteLine(u.email);
```

**List users by Tag, Segment, Company**
```cs
Dictionary<String, String> parameters = new Dictionary<string, string>();
parameters.Add("segment_id", "57553e93a32843ca09000277");
Users users = usersClient.List(parameters);
```

**List users via the Scroll API**
```cs
Users users = usersClient.Scroll();
String scroll_param_value = users.scroll_param;
Users users = usersClient.Scroll(scroll_param_value);
```

**Delete a user**
```cs
usersClient.Delete("100300231"); // with intercom generated user's id
usersClient.Delete(new User() { email = "example@example.com" });
usersClient.Delete(new User() { user_id = "my_id" });
```

**Update User's LastSeenAt (multiple ways)**
```cs
User user = usersClient.UpdateLastSeenAt("100300231");
User user = usersClient.UpdateLastSeenAt(new User() { id = "100300231" });
User user = usersClient.UpdateLastSeenAt("100300231", 1462110718);
User user = usersClient.UpdateLastSeenAt(new User() { id = "100300231" }, 1462110718);
```

**Increment User's Session**
```cs
usersClient.IncrementUserSession(new User() { id = "100300231" });
```

**Removing User's companies**
```cs
User user = usersClient.RemoveCompanyFromUser("100300231", new List<String>() { "true_company" });
```

*** 

### Contacts

**Create ContactsClient instance**
```cs
ContactsClient contactsClient = new ContactsClient(new Authentication("MyPersonalAccessToken"));
```

**Create a contact**
```cs
Contact contact = contactsClient.Create(new Contact() { });
Contact contact = contactsClient.Create(new Contact() { name = "lead_name" });
```

**View a contact (by id, or user_id)**
```cs
Contact contact = contactsClient.View("100300231");
Contact contact = contactsClient.View(new Contact() { id = "100300231" });
Contact contact = contactsClient.View(new Contact() { user_id = "my_lead_id" });
```

**Update a contact (by id, or user_id)**
```cs
Contact contact = contactsClient.Update(
                    new Contact()
                    {   
                        email = "example@example",
                        companies = new List<Company>() { new Company() { company_id = "new_company" } }
                    });
```

**List contacts and iterate through them**
Limited to up to 10k records, if you want to list more records please use the Scroll API
```cs
Contacts contacts = contactsClient.List();

foreach (Contact c in contacts.contacts)
    Console.WriteLine(c.email);
```

**List contacts by email**
```cs
Contacts contacts = contactsClient.List("email@example.com");
```

**List contacts via Scroll API**
```cs
Contacts contacts = contactsClient.Scroll();
String scroll_param_value = contacts.scroll_param;
Contacts contacts = contactsClient.Scroll(scroll_param_value);
```

**Convert a contact to a User**
Note that if the user does not exist they will be created, otherwise they will be merged.
```cs
User user = contactsClient.Convert(contact, new User() { user_id = "120" });
```

**Delete a contact**
```cs
contactsClient.Delete("100300231");
contactsClient.Delete(new Contact() { id = "100300231" });
contactsClient.Delete(new Contact() { user_id = "my_id" });
```

***

### Visitors

**Create VisitorsClient instance**
```cs
VisitorsClient visitorsClient = new VisitorsClient(new Authentication("MyPersonalAccessToken"));
```

**View a visitor**
```cs
Visitor visitor = VisitorsClient.View("573479f784c5acde6a000575");
```

**View a visitor by user_id**
```cs
Dictionary<String, String> parameters = new Dictionary<string, string>();
parameters.Add("user_id", "16e690c0-485a-4e87-ae98-a326e788a4f7");
Visitor visitor = VisitorsClient.View(parameters);
```

**Update a visitor**
```cs
Visitor visitor = VisitorsClient.Update(visitor);
```

**Delete a visitor**
```cs
Visitor visitor = VisitorsClient.Delete(visitor);
```

**Convert to existing user**
```cs
Visitor visitor = VisitorsClient.ConvertToUser(visitor, user);
```

**Convert to new user**
```cs
Visitor visitor = VisitorsClient.ConvertToUser(visitor, new User(){ user_id = "25" });
```

**Convert to contact**
```cs
Visitor visitor = VisitorsClient.ConvertToContact(visitor);
```

***

### Companies

**Create CompanyClient instance**
```cs
CompanyClient companyClient = new CompanyClient(new Authentication("MyPersonalAccessToken"));
```

**Create a company**
```cs
Company company = companyClient.Create(new Company());
Company company = companyClient.Create(new Company() { name = "company_name" });
```

**View a company**
```cs
Company company = companyClient.View("100300231");
Company company = companyClient.View(new Company() { id = "100300231" });
Company company = companyClient.View(new Company() { company_id = "my_company_id" });
Company company = companyClient.View(new Company() { name = "my_company_name" });
```

**Update a company**
```cs
Company company = companyClient.Update(
                    new Company()
                    {   
                        company_id = "example@example",
                        monthly_spend = 100
                    });
```

**List companies**
<br>Limited to up to 10k records, if you want to list more records please use the Scroll API
```cs
Companies companies = companyClient.List();
```

**List companies via Scroll API**
```cs
Companies companies = companyClient.Scroll();
String scrollParam = companies.scroll_param;
Companies companies = companyClient.Scroll(scrollParam);

foreach (Company c in companies.companies)
    Console.WriteLine(c.name);
```

**List a Company's registered users**
```cs
Users users = companyClient.ListUsers(new Company() { id = "100300231" });
Users users = companyClient.ListUsers(new Company() { company_id = "my_company_id" });
```

***

### Admins

**Create AdminsClient instance**
```cs
AdminsClient adminsClient = new AdminsClient(new Authentication("MyPersonalAccessToken"));
```

**View an admin**
```cs
Admin admin = adminsClient.View("100300231");
Admin admin = adminsClient.View(new Admin() { id = "100300231" });
```

**List admins**
```cs
Admins admins = adminsClient.List();
```

***

### Tags

**Create TagsClient instance**
```cs
TagsClient tagsClient = new TagsClient(new Authentication("MyPersonalAccessToken"));
```

**Create a tag**
```cs
Tag tag = tagsClient.Create(new Tag() { name = "new_tag" });
```

**List tags**
```cs
Tags tags = tagsClient.List();
```

**Delete a tag**
```cs
tagsClient.Delete(new Tag() { id = "100300231" });
```

**Tag User, Company or Contact**
```cs
tagsClient.Tag("new_tag", new List<Company>() { new Company(){ company_id = "blue" } });
tagsClient.Tag("new_tag", new List<Company>() { new Company(){ id = "5911bd8bf0c7223d2d1d045d" } });
tagsClient.Tag("new_tag", new List<Contact>() { new Contact(){ id = "5911bd8bf0c7446d2d1d045d" } });
tagsClient.Tag("new_tag", new List<User>() { new User(){ id = "5911bd8bf0c7446d2d1d045d", email = "example@example.com", user_id = "25" } });
```

**Untag User, Company or Contact**
```cs
tagsClient.Untag("new_tag", new List<Company>() { new Company(){ company_id = "1000_company_id" } });
tagsClient.Untag("new_tag", new List<Contact>() { new Contact(){ id = "5911bd8bf0c7223d2d1d045d" } });
tagsClient.Untag("new_tag", new List<User>() { new User(){ user_id = "1000_user_id" } });
```

***

### Segments

**Create SegmentsClient instance**
```cs
SegmentsClient segmentsClient = new SegmentsClient(new Authentication("MyPersonalAccessToken"));
```

**View a segment (by id)**
```cs
Segment segment = segmentsClient.View("100300231");
Segment segment = segmentsClient.View(new Segment() { id = "100300231" });
```

**List segments**
```cs
Segments segments = segmentsClient.List();
```

***

### Notes

**Create NotesClient instance**
```cs
NotesClient notesClient = new NotesClient(new Authentication("MyPersonalAccessToken"));
```

**Create a note (by User, body and admin_id)**
```cs
Note note = notesClient.Create(
    new Note() {
    author = new Author() { id = "100300231_admin_id" },
    user =  new User() { email = "example@example.com" },
    body = "this is a new note"
});

Note note = notesClient.Create(new User() { email = "example@example.com" }, "this is a new note", "100300231_admin_id");
```

**View a note**
```cs
Note note = notesClient.View("2001");
```

**List User's notes**
```cs
Notes notes = notesClient.List(new User() { id = "100300231"});

foreach (Note n in notes.notes)
    Console.WriteLine(n.user.name);
```

***

### Events

**Create EventsClient instance**
```cs
EventsClient eventsClient = new EventsClient(new Authentication("MyPersonalAccessToken"));
```

**Create an event**
```cs
Event ev = eventsClient.Create(new Event() { user_id = "1000_user_id", email = "user_email@example.com", event_name = "new_event", created_at = 1462110718  });
```

**Create an event with Metadata (Simple, MonetaryAmounts and RichLinks)**
```cs
Metadata metadata = new Metadata();
metadata.Add("simple", 100);
metadata.Add("simple_1", "two");
metadata.Add("money", new Metadata.MonetaryAmount(100, "eur"));
metadata.Add("richlink", new Metadata.RichLink("www.example.com", "value1"));

Event ev = eventsClient.Create(new Event() { user_id = "1000_user_id", email = "user_email@example.com", event_name = "new_event", created_at = 1462110718, metadata = metadata  });
```

**List events by user**
```cs
Events events = eventsClient.List(new User() { user_id = "my_id" });
```

***

### Counts

**Create CountsClient instance**
```cs
CountsClient countsClient = new CountsClient(new Authentication("MyPersonalAccessToken"));
```

**Get AppCount**
```cs
AppCount appCount = countsClient.GetAppCount();
```

**Get Specific Counts**
```cs
ConversationAppCount conversationAppCount = countsClient.GetConversationAppCount();
ConversationAdminCount conversationAdminCount = countsClient.GetConversationAdminCount();
CompanySegmentCount companySegmentCount = countsClient.GetCompanySegmentCount();
CompanyTagCount companyTagCount = countsClient.GetCompanyTagCount();
CompanyUserCount companyUserCount = countsClient.GetCompanyUserCount();
UserSegmentCount userSegmentCount = countsClient.GetUserSegmentCount();
UserTagCount userTagCount = countsClient.GetUserTagCount();
```

***

### Conversations

**Create ConversationsClient instance**
```cs
ConversationsClient conversationsClient = new ConversationsClient(new Authentication("MyPersonalAccessToken"));
```

**View any type of conversation**
```cs
conversationsClient.View("100300231");
conversationsClient.View("100300231", displayAsPlainText: true);
```

**Create AdminConversationsClient instance**
```cs
AdminConversationsClient adminConversationsClient = new AdminConversationsClient(new Authentication("MyPersonalAccessToken"));
```

**Create Admin initiated Conversation**
```cs
AdminConversationMessage admin_message =
    adminConversationsClient.Create(new AdminConversationMessage(
            from: new AdminConversationMessage.From("1000_admin_id"),
            to: new AdminConversationMessage.To(id: "1000_user_id"),
            message_type: AdminConversationMessage.MessageType.EMAIL,
            template: AdminConversationMessage.MessageTemplate.PERSONAL,
            subject: "this is a subject",
            body: "this is an email body"));
```

**Create Admin initiated Conversation's reply**
```cs
AdminConversationReply admin_reply =
    adminConversationsClient.Reply(
        new AdminConversationReply(
            conversationId: "1000_conversation_id",
            adminId: "1000_admin_id",
            messageType: AdminConversationReply.ReplyMessageType.COMMENT,
            body: "this is a reply body"));
```

**Create UserConversationsClient instance**
```cs
UserConversationsClient userConversationsClient = new UserConversationsClient(new Authentication("MyPersonalAccessToken"));
```

**Create User initiated Conversation**
```cs
UserConversationMessage user_message =
    userConversationsClient.Create(
        new UserConversationMessage(
            from: new UserConversationMessage.From(id: "1000_user_id"),
            body: "this is a user's message body"));
```

**Create User initiated Conversation's reply**
```cs
UserConversationReply user_reply =
    userConversationsClient.Reply(
        new UserConversationReply(
            conversationId: "1000_conversation_id",
            body: "this is a user's reply body",
            attachementUrls: new List<String>() { "www.example.com/example.png", "www.example.com/example.txt" }));
```

***

### Webhooks & Pagination

Not yet supported by these bindings.

## Todo

- [ ] Increase test coverage
- [ ] Support Pagination
- [ ] Support Webhooks
- [ ] Support Async
- [ ] Have 100% feature parity with curl

## Pull Requests

* **Add tests!** Your patch won't be accepted if it doesn't have tests.
* **Document any change in behaviour.** Make sure the README and any other relevant documentation are kept up-to-date.
* **Create topic branches.** Don't ask us to pull from your master branch.
* **One pull request per feature.** If you want to do more than one thing, send multiple pull requests.
* **Send coherent history.** Make sure each individual commit in your pull request is meaningful. If you had to make multiple intermediate commits while developing, please squash them before sending them to us.**
