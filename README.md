# intercom-dotnet

.NET bindings for the [Intercom API](https://api.intercom.io/docs)

 - [Installation](#installation)
 - [Resources](#resources)
 - [Authorization](#authorization)
 - [Usage](#usage)


## Add a dependency


### nuget

install command

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
usersClient.Update(new User() { 
email = "example@example.com", 
companies = new List<Company>() { new Company() { company_id = "new_company" } } });

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
contact = contactsClient.Create(new Contact() { name = "lead_name" });

// View a contact (by id, or user_id)
Contact contact1 = contactsClient.View("100300231");
contact1 = contactsClient.View(new Contact() { id = "100300231" });
contact1 = contactsClient.View(new Contact() { user_id = "my_lead_id" });

// Update a contact (by id, or user_id)
contactsClient.Update(
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
company = companyClient.Create(new Company() { name = "company_name" });

// View a contact (by id, or user_id)
Company company1 = companyClient.View("100300231");
company1 = companyClient.View(new Company() { id = "100300231" });
company1 = companyClient.View(new Company() { company_id = "my_company_id" });
company1 = companyClient.View(new Company() { name = "my_company_name" });

// Update a contact (by id, or user_id)
companyClient.Update(
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

### Segments

### Notes

### Counts

### Tags

### Events

### Conversations

### Webhooks

### Bulk APIs


