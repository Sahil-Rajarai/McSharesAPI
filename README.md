# McSharesAPI
* Web API to support the upload of XML files

## Controllers
* Upload XML file (requires Xml file to be in the same format)
* Read XML file, deserialize file contents to Customer class and save valid customer details in database
* Perform Creation, Updates, Retrieval of all records, Retrieval indiviual record by customer Id, Search Customer by name using Dependency Injection
* Log any error and store all logs [LogsController]
* Allow the downloads of CSV file containing customer details using the CsvHelper package

## Services
* All Validations are done in the class CustomerService
* All Static variables which are hard-coded are saved in the StaticVariables class 
* CustomerMapper class is used for mapping of customers to get only specific fields that are returned in Get Request

## Repository
* Customer Details and Logs stored in Memory (Only need to change the InMemoryCustomerRepository class and InMemoryLoggerRepository class if another Db is used)
* InMemoryCustomerRepository class and InMemoryLoggerRepository class implements the ICustomerRepository and ILoggerRepository respectively which is used in the ontroller class (Dependency Injection)
* Controller calls the Repository class to get or modify customer records depending on thecan  request
* Search on database can be optimised using CQRS with eventstore and elasticsearch in future

## Models
* Contains the Customer and Log class to be used as the application business logic and the database access logic

## How to
* To test the application, please download the Postman collection on the following link - [![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/f9518d385e9b68e79eeb)
* Follow the steps below in order to upload and read the xml file using the Web Api :

1. Start the Web Api - dotnet run
1. Register a new XML file - "UploadFile" POST request on Postman 
   * Switch to the Body tab and choose form-data.
   * Add the key "file", select File from the next dropdown and then browse for the XML file 
   * Send the request
   * A directory named data will be created which will contain the XML file
   * List of valid customers in the file will be saved in the database
   * A 201 Created response signals a successful creation of customers
1. Run the "GetAllCustomersDetails" or "GetAllCustomersEntity" request to get a list of all valid customers
   * A 200 Ok response signals a successful retrieval
1. Similarly, run the "GetCustomerDetailsById" or "GetCustomerEntityById" request by specifying an Id in the URL(/{id}) to get the details of one customer
   * A 200 Ok response signals a successful retrieval
1. Run the "UpdateCustomer" PUT request by specifying an Id in the URL(/{id}) to update the details of one customer
   * Switch to the Body tab and choose RAW
   * Choose JSON in the right dropdown and enter a valid JSON (containing the CustomerEntity class fields) for this particular customer - The JSON can contain the CustomerId
   * JSON example - 
     {
      "customerName": "Sahil",
      "dateOfBirth": "",
      "dateIncorp": "01/07/2012",
      "customerType": "Corporate",
      "numShares": 4000,
      "sharePrice": 20.2,
      "balance": 36800
    }
   * A 200 Ok response signals a successful update
1. Run the "SearchCustomers" to get a list of Customers who have name similar to the one passed as parameter
   * Switch to Params in Postman 
   * Add the key "name" and any string on the value
   * Send the request
   * A list of Customers with similar name will be returned
   * A 200 Ok response signals a successful retrieval of customers
1. Run the "GetCSVFile" to get a valid CSV file
   * To download the CSV file - Go to Save response on Postman in the response section and choose Save to File 
   * A valid CSV file named "Reports.csv" will be downloaded on your local machine
   * A 200 Ok response signals a successful retrieval of customers and conversion to a CSV file

