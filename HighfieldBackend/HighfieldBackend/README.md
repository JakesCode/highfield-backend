# This is the C# ASP.NET Backend to my Highfield Technical Test.

Important: During my development, the port **44399** was always automatically selected. If a different port ends up being selected when you run it, please update the Frontend so that it points to the same port. (Instructions can be found on the Frontend's README).

## What I've done
I have created a Web API V2 application with one controller (`UserController`), which contains two routes - the default route (`/`) which simply displays an 'up and running' message, and the user endpoint (`/api/users`). As well as this, there are multiple functions within this controller which perform the logic of calculating the top colours, ages plus twenty etc.

The various classes live in a Classes folder and are instantiated when returning the result to the frontend.