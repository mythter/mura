# Data Types

|**Data type**|**Description**|**Example**|
|---------|-----------|-------|
|`String`|UTF-8 encoded text string|`"Alice"`|
|`Integer`|32-bit signed integer (int32)|`25`|
|`Long`|64-bit signed integer (int64) for large values|`NumberLong(9999999999)`|
|`Double`|64-bit IEEE 754 floating-point number|`19.99`|
|`Decimal128`|128-bit high-precision decimal|`NumberDecimal("99.99")`|
|`Boolean`|Logical true / false value|`true`|
|`ObjectId`|12-byte unique identifier, auto-generated for \_id fields|`ObjectId('69d2bdb643fd93140a3e2a90')`|
|`Date`|Date and time stored as milliseconds since Unix epoch|`new Date("2024-01-01")`|
|`Array`|Ordered list of values of any type|`["js", "db"]`|
|`Object`|Embedded (nested) document|`{ "city": "Kyiv" }`|
|`Null`|Represents the absence of a value|`null`|
|`Binary`|Raw binary data (files, hashes)|`BinData(0, "base64==")`|
|`Regex`|Regular expression stored as a native BSON type|`/^abc/i`|
|`Timestamp`|Internal MongoDB type used for replication (not for storing dates)|`Timestamp(1, 1)`|
|`MinKey / MaxKey`|Special values that compare lower / higher than any other BSON value|`used in indexes`|
|`Symbol`|Deprecated string-like type – not recommended for use|`Symbol("name")`|
|`JavaScript`|Stores a JavaScript function or code snippet directly in a document. Historically used with $where queries – largely deprecated in modern MongoDB due to security concerns|`{ "$code": "function() { return this.age > 18; }" }`|
