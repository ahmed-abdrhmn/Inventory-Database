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

## Inventory (Correspond to InventoryInDetail)
### Get all inventory documents
    GET http://localhost:5230/api/inventory
### Get inventory document by ID
    GET http://localhost:5230/api/inventory/:id
### Update inventory document
    PUT http://localhost:5230/api/inventory/:id

with JSON body like this:
```
{
    "inventoryInHeaderId": 1,
    "serial": 123,
    "itemId": 3,
    "packageId": 3,
    "batchNumber": "2323232323",
    "serialNumber": "4545454545",
    "expireDate": "2024-05-12",
    "quantity": 12.400000,
    "consumerPrice": 19.200000
}
```
### Create new inventory document
    POST http://localhost:5230/api/inventory/

with JSON body the same format as update

### Delete inventory document
    DELETE http://localhost:5230/api/inventory/:id

## Header (Corresponding to InventoryInHeader)
### Get all headers
    GET http://localhost:5230/api/headers
### Get header by ID
    GET http://localhost:5230/api/header/:id
### Update header
    PUT http://localhost:5230/api/header/:id

with JSON body like this:
```
{
    "BranchId": 1,
    "DocDate": "2030-01-01",
    "Reference": "123456",
    "Remarks": "Performing Well"
}
```
### Create new header
    POST http://localhost:5230/api/header/

with JSON body the same format as update

### Delete header
    DELETE http://localhost:5230/api/header/:id

## Branch/Package/Item
The branch, package, and item endpoints are similar to each other. Here I will document the branch endpoint but it will apply to the other endpoints as well.

### Get all branches
    GET http://localhost:5230/api/branch

### Get branch by ID
    GET http://localhost:5230/api/branch/:id

### Update branch
    PUT http://localhost:5230/api/branch/:id

with JSON body like this:
```
{
    "Name":"New Branch Name"
}
```
### Create new branch
    POST http://localhost:5230/api/branch/

with JSON body the same format as update
### Delete branch
    DELETE http://localhost:5230/api/branch/:id