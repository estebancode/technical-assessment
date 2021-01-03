# Technical-assessment
Technical assessment 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

## Prerequisites

After cloning the repository:
 * Make sure to open the project with __*visual studio 2019*__ or check if you have to install __*Dotnet 3.1*__.
 * Make sure that you have a good connection to internet (to use external services).
 * In case that you want to change the connection string to make sure to change it 
   in the file __**appSettings.json*__ and run the migration with the command __*Update-database*__ in the  Package manage console.
 * Make sure that you set the project __*Technical.Assessment.Api*__ like __*"Set as Startup Project"*__ .
 * Make sure that you have the path of __*Swagger*__ in the file __*launchSettings.json*__ .
    ```json
        "profiles": {
            "IIS Express": {
            "commandName": "IISExpress",
            "launchBrowser": true,
            "launchUrl": "swagger",
            "environmentVariables": {
                "ASPNETCORE_ENVIRONMENT": "Development"
            }
            },
            "Technical.Assessment.Api": {
            "commandName": "Project",
            "launchBrowser": true,
            "launchUrl": "swagger",
            "applicationUrl": "https://localhost:5001;http://localhost:5000",
            "environmentVariables": {
                "ASPNETCORE_ENVIRONMENT": "Development"
            }
            }
        }
    ```

## Installing

Before to run the project you must build applying the next command:
```bash
dotnet build
```
or with the option **Build** or __CTRL + B__ in the visual studio.

To run the project you can apply the command:
```bash
dotnet run
``` 
or with the option **IIS Express** or __F11__ in the visual studio.


>Once, the API has been launched you can see the Swagger page with all documentation.

## Usage

 To use the all API you need an username and password to get a token 

* To create an username and password make sure to use the method **Respondent -> post**
* To get a token to make sure to use the method **Authenticate -> post** with his username and password
* Once you get the token to make sure to set in the option __*Authorize*__ of Swagger page.

## Architecture

Type: Hexagonal 

 * __*Domain:*__ This layer contains the core business logic.
 * __*Infrastructure:*__ this layer implements how domain interacts with the external world. Communicate with repositories,queues, AD, etc.
 * __*API:*__ This is layer of presentation.
 * __*Unit test:*__ This layer is where you can test methods by which individual units of source code from domain's layer.
 * __*Integration test (API test):*__ Here, all components are integrated together at once and then tested. For example, We tested layer by layer, the layer of access to data, the layer of logic, and the layer of presentation.

 ## Designs Pattern
 
 * Data builder pattern

 ## Missing points

 * "When any mutation like Insert, Update or Delete is performed on the question_order table, you should correctly rearrange the question’s order in the survey. Keep in mind this should be reflected on that question_order table."

 ## Enhancement opportunities

 * Replace the ID the type INT for GUID to prevent predictable identifiers

