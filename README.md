# Movie Rama Assignment

## Introduction

This assignment has been implemeted as a SPA application by using the following technologies:

- Front-end
  - React
  - Redux
- Back-end
  - .Net Core
- Database server
  - SQL Server

## Running instance of application

You can see a running instance of the deliverable in the following location: [Movierama](https://movierama.azurewebsites.net/)

A public API is also available at: [API](https://movieramaapi.azurewebsites.net/api/movies)

## Installation

If you wish to run this project locally please follow the instructions listed below.

### Front-end

Building the [front end application](movierama-app) requires [npm](https://www.npmjs.com/get-npm) to be installed.

In the project directory, you can run:

#### `npm install`

To install all project dependencies and:

#### `npm start`

To start the app at the local web server. This opens the app at: [http://localhost:3000](http://localhost:3000).

For more detailed instructions see the project's [readme](movierama-app)

In development mode, the SPA application requires a back-end api to serve the data at [http://localhost:49717/api](http://localhost:49717/api)

Please see the following instructions that describe how to setup the API locally.

### Back-end

In order to run the api project in development mode, visual studio 15.7 or later is required (in order to have the .net core 2.1 installed)

Open the [MovieRama.sln](MovieRama.sln) file and run the MovieRama.API project. This starts a local instance of the api at [http://localhost:49717/api](http://localhost:49717/api)

In development mode, the back end service requires an active MovieRama database at localhost (sql server default port). If you need to specify an other database name or server please modify the connection string option located at the [appsettings.json](./MovieRama.Api/appsettings.json).

Please see the following instructions that describe how to setup the database. 

### Database

In order to set up the database please run the scripts contained in the [database](database) folder.

These scripts create a new database called MovieRama. If you need a different name for you database please edit the scripts appropriatly.
