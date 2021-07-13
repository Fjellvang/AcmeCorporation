#Acme Copororation

This is a simple project for acme corporation where in users supplied with a valid serial can enter a draw. Admins can see all entries when logged in. 

#### Technologies:
* ASP.Net Core 5
* MS EntityFramework
* NUnit
* Boostrap
* Moq
* React

#### Getting Started:
To Run the project you need the following:
1. Ensure .Net 5 is installed on your machine.
2. Ensure Node is installed on your machine
3. Open ``AcmeCorpotation.sln``
4. Acquire fresh MSSQL database.
	* Once acquired, input your connection string in appsettings.json.
5. Run the project.
	* On the inital run ``validGuids.txt`` will be updated with the correct guids seede to the DB
	* I've created a sample admin login to inspect submissions, userid: ``admin@admin.dk`` password: ``!QAZxsw2``

#### Database Migrations
Database migration are applied automatically when you run the project, if the connection string is configured correctly.


##Improvements and shorcomings

* Better Frontend
	* My emphasis has been on the Backend, hence the frontend implementation is pretty rudimentary. Proper error handling and UX to show the errors to the user would be expected.

* Issue with Authorize attributes and policies.
	* I encountered a configuration issue with how i handle claims, policies in Identity Provider. I made a quick fix to ensure the endpoints, but given more time i would solve it more elegantly. I assume its an issue with my jwt token authorization.
