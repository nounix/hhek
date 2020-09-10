# Battleship

This is an implementation of the game [battleship](https://en.wikipedia.org/wiki/Battleship).

The project is designed for playing the game in an online multiplayer 1 vs 1 mode.
One server instance you can host multiple games and 2 players can join 1 game.

# Dev notes:
The project has 4 parts each written in c#, developed & tested with the dotnet core 3 sdk:
- framework
- models
- server
- client

## Framework:
The framework is the heart of the project and the most interesting part.
Its a fullstack MVC framework for c# based console applications, which communicate via TCP.
It even have its own dependency injection methods, which keeps the prod code lean and more beautiful.

## Models:
The models are used as entities for the DB and are identified via GUID.
The idea behind adding the models as dependecy is, that client and server have the same version of each
model.

## Server:
The server have request routes, which is the API.
Each route is an Event and also automatically created at runtime and managed by the Eventhandler.
While implementing, I was inspired by REST and GRPC.
The server also have data access objects for the DB operations.

## Client:
The client has controllers and every controller has its view.
And they are all automatically created at runtime.
In the client you re jumping from view to view.


### License
This Project is released under GNU GPLv3 licence. Copyright (c) iPUSH (Martin Muenning).