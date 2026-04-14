# Delete

### Delete one document

````
db.my_colelction.deleteOne({ field1: "value1" })
````

### Delete multiple documents

````
db.my_colelction.deleteMany({ field2: 99 })
````

### Delete all documents

````
db.my_colelction.deleteMany({})
````

### Find and Delete

Finds a document, deletes it, and returns the deleted document

````
db.my_colelction.findOneAndDelete({ field1: "value1" })
````
