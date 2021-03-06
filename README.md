# JWT WebAPI Template

## What is this?

While developing a .Net Framework WebAPI for a client, I found few tutorials / little documentation that instructed how to set up custom authentication with WebAPI.  I was asked to develop [JWT](https://jwt.io/)-based authentication for a WebAPI.  I wanted to learn Identity and how it works (so that part is not custom) but I found the pieces necessary to do so and will document them.  

## Documentation:

This project creates an `AuthorizationFilterAttribute` used as `[JwtAuthorizationFilter]` on whatever ApiController that it is needed.  I have not yet injected this as standard, but the basic task (once all the Todo's have been fixed, see notes below) is login with `POST` of username / password to https://localhost:44340/api/Token to receive the JWT and then it can be put in the authorization header of future requests to other api calls.

Aside from the above, there is currently not much documentation, but you can review the commit history for changes that I made to a base MVC + WebAPI boilerplate code.   The "customization" can be seen by looking at the Task List in visual studio and looking through the TODOs in the comments.   I appreciate andy feedback or advice for making this better.   I have removed a custom session management piece from the original project I worked on. Also, this is missing an account management controller so you would have to seed your database with a user to test it.


