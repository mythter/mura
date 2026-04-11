### Insert one document

````
db.my_colelction.insertOne({
	field1: "value1",
	field2: 30
})
````

### Insert multiple documents

````
db.my_colelction.insertMany([
	{ field1: "value1", field2: 25 },
	{ field11: "value11", field22: 28 }
])
````

### Unordered insert

ordered: false – continues inserting even if some documents fail

````
db.my_colelction.insertMany([...], { ordered: false })
````

### Insert or update (upsert)

Inserts a new document if not found; updates the existing one if found

````
db.my_colelction.updateOne(
	{ field1: "value1" },
	{ $set: { field2: 99 } },
	{ upsert: true }
)
````
