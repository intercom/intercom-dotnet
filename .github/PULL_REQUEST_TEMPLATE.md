#### Why?
I am making this change to allow for the use of Articles Data Model within the Intercom API
Requires Intercom API 2.0+

#### How?
Added Compatibility for Articles Object Model
(Client, Model (Singular and list))
Noted as only available in Intercom API v2.0+
Added Obsolete flag on UsersClient as it is deprecated in Intercom API 2.0+
Incorporated Pull Request for DateTimeOffset conversions
Added appropriate test cases for Articles and DateTime Converter
