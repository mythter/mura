### Show all collections in the current database

````
show collections
````

### Create collection

````
db.CreateCollection("my_collection")
````

````
db.createCollection("my_collection", { capped: true, size: 5242880, max: 5000 })
````

### Rename collection

````
db.my_collection.renameCollection("new_collection")
````

### Count documents

````
db.my_collection.countDocuments()
````

````
db.my_collection.countDocuments({ active: true })
````

### Delete collection

````
db.my_collection.drop()
````

### Show collection statistics

````
db.my_collection.stats()
````
