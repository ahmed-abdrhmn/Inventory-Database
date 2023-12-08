# Introduction
This is a rest crud api for accessing inventory data that is persisted in a database

# Requirements
1. You must have .NET Core 7 SDK installed on your machine. Here, I will the dotnet CLI commands. [see here](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
2. Install the entity framework cli tools. [see here](https://learn.microsoft.com/en-us/ef/core/cli/dotnet#installing-the-tools) 
3. You must have MySQL installed and setup on your machine. 

# Setting up This Project
Do the following steps:
1. Clone this repository
2. Open Startup/appsettings.json and replace the connection string there with that of your database
3. Open a terminal in the root directory of the project
4. To apply the migrations to your database enter the following command in the terminal:

       dotnet ef database update --startup-project Startup --project "Infrastructure Layer"
    > Note: the migration will pre populate your database with objects, so you can immediately begin using the GET endpoints.

5. To run the program, enter this command in the terminal:

        dotnet run --project Startup

# Endpoints

> Note: If you have postman in your machine, you can import InventoryDb.postman_collection.json to run prewritten sample requests.

## Inventory
### Get all inventory documents
    GET http://localhost:5230/api/inventory
### Get inventory document by ID
    GET http://localhost:5230/api/inventory/:id
### Update inventory document
    PUT http://localhost:5230/api/inventory/:id

with JSON body like this:
```
{
    "branch": {
        "name": "Western Branch",
        "id": 3
    },
    "docDate": "2023-12-07",
    "reference": "Another Random Reference",
    "remarks": "All Remarks",
    "inventoryInDetails": [
        {
            "serial": 123,
            "item": {
                "name": "Tomato",
                "id": 3
            },
            "package": {
                "name": "Metal Container",
                "id": 3
            },
            "batchNumber": "67676767676",
            "serialNumber": "9090909090",
            "expireDate": "2030-01-11",
            "quantity": 78.900000,
            "consumerPrice": 0.120000,
            "id": 2
        },
        ...
    ]
}
```
For the Package, Item, and Branch objects, the system will check if the given id exists already. If it does, the system will update it, otherwise it will create a new one with the Id you provided. If you don't provide an id, a new object with automatic id will be created. Right now there is no way to just delete one of these objects.

> Note: You should take the JSON from the GetbyId endpoint, make the changes you want, then send it to this endpoint.

### Create new inventory document
    POST http://localhost:5230/api/inventory/

with JSON body the same format as update

### Delete inventory document
    DELETE http://localhost:5230/api/inventory/:id