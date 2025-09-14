# Data Types

## String Data Types

| Data type | Description | Max char length | Storage |
|---|---|---|---|
| char(n) | Fixed-length non-Unicode character data (n must be between 1 and 8000) | 8,000 | n bytes (uses one byte for each character) |
| varchar(n) | Variable-length non-Unicode character data (n must be between 1 and 8000) | 8,000 | n bytes + 2 bytes |
| varchar(max) | Variable-length non-Unicode character data |  | up to 2 GB |
| nchar(n) | Fixed-length Unicode character data (n must be between 1 and 4000) | 4,000 | 2 * n bytes (uses two bytes for each character) |
| nvarchar(n) | Variable-length Unicode character data (n must be between 1 and 4000) | 4,000 | 2 * n bytes + 2 bytes (uses two bytes for each character) |
| nvarchar(max) | Variable-length Unicode character data |  | up to 2 GB |
| binary(n) | Fixed-length binary data (n must be between 1 and 8000) | 8,000 | n bytes |
| varbinary(n) | Variable-length binary data (n must be between 1 and 8000) | 8,000 | actual length of data entered + 2 bytes |
| varbinary(max) | Variable-length binary data |   | 2GB |

## Numeric Data Types

| Data type | Description | Storage |
|---|---|---|
| bit | Integer that can be 0, 1, or NULL |   |
| tinyint | Allows whole numbers from 0 to 255 | 1 byte |
| smallint | Allows whole numbers between -32,768 and 32,767 | 2 bytes |
| int | Allows whole numbers between -2,147,483,648 and 2,147,483,647 | 4 bytes |
| bigint | Allows whole numbers between -9,223,372,036,854,775,808 and 9,223,372,036,854,775,807 | 8 bytes |
| decimal(p,s) | Fixed precision and scale numbers.<br><br> Allows numbers from -10^38 +1 to 10^38 –1.<br><br> The p parameter indicates the maximum total number of digits that can be stored (both to the left and to the right of the decimal point). p must be a value from 1 to 38. Default is 18.<br><br> The s parameter indicates the maximum number of digits stored to the right of the decimal point. s must be a value from 0 to p. Default value is 0 | 5-17 bytes |
| numeric(p,s) | Fixed precision and scale numbers.<br><br> Allows numbers from -10^38 +1 to 10^38 –1.<br><br> The p parameter indicates the maximum total number of digits that can be stored (both to the left and to the right of the decimal point). p must be a value from 1 to 38. Default is 18.<br><br> The s parameter indicates the maximum number of digits stored to the right of the decimal point. s must be a value from 0 to p. Default value is 0 | 5-17 bytes |
| smallmoney | Monetary data from -214,748.3648 to 214,748.3647 | 4 bytes |
| money | Monetary data from -922,337,203,685,477.5808 to 922,337,203,685,477.5807 | 8 bytes |
| float(n) | Floating precision number data from -1.79E + 308 to 1.79E + 308.<br><br> The n parameter indicates whether the field should hold 4 or 8 bytes. float(24) holds a 4-byte field and float(53) holds an 8-byte field. Default value of n is 53. | 4 or 8 bytes |
| real | Floating precision number data from -3.40E + 38 to 3.40E + 38 | 4 bytes |

## Date and Time Data Types

| Data type | Description | Storage |
|---|---|---|
| datetime | From January 1, 1753 to December 31, 9999 with an accuracy of 3.33 milliseconds | 8 bytes |
| datetime2 | From January 1, 0001 to December 31, 9999 with an accuracy of 100 nanoseconds | 6-8 bytes |
| smalldatetime | From January 1, 1900 to June 6, 2079 with an accuracy of 1 minute | 4 bytes |
| date | Stores a date only. From January 1, 0001 to December 31, 9999 | 3 bytes |
| time | Stores a time only to an accuracy of 100 nanoseconds | 3-5 bytes |
| datetimeoffset | The same as datetime2 with the addition of a time zone offset | 8-10 bytes |
| timestamp | Stores a unique number that gets updated every time a row gets created or modified. The timestamp value is based upon an internal clock and does not correspond to real time. Each table may have only one timestamp variable |   |

## Other Data Types

| Data type | Description |
|---|---|
| sql_variant | Stores up to 8,000 bytes of data of various data types, except text, ntext, and timestamp |
| uniqueidentifier | Stores a globally unique identifier (GUID) |
| xml | Stores XML formatted data. Maximum 2GB |
| cursor | Stores a reference to a cursor used for database operations |
| table | Stores a result-set for later processing |
