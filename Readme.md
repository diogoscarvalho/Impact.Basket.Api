# Code Challenge

## Description
We expect you to invest 2 hours on this Code Challenge, we don't expect you to meet all expectations. We advise you to find a quiet time and space to be
focused on the Code Challenge, so that you can work without interruptions during this period.
Use our Code Challenge API to build your own Basket API. Assume your Basket API is going to be used by end-users via a typical online shopping
frontend. Therefore, your Basket API should support the regular operations of an online shopping cart, e.g., add product(s) to basket, remove product(s)
from basket, decrease and increase product quantity, and also, submit an order using the basket. Note that the mentioned operations do not correspond
directly to endpoints. It is up to you to decide which endpoints make sense in your Basket API.

To support the development of your Basket API, we provide our own Code Challenge API. You can have a look at it's Swagger UI available here <Git Hub Repository>

From these 10.000 products, **only the top ranked 100 products should be allowed to be bought online.**
The Code Challenge API requires authentication. You can obtain a Token with the Login endpoint using your email. However, to simplify your
implementation, assume that any clients of your API should not need authentication, hence they should not insert an email address on any of your
endpoints. The objective of authentication in the Code Challenge API is to authenticate your API alone.
Lastly, once the end-user has selected the desired products, you should allow him/her to submit the basket, thus calling our CreateOrder endpoint.
Beyond the endpoints you designed for the Basket API, you should also provide the following endpoints:

- Return the top ranked 100 products.
- Return paginated results of the product catalog ordered by price in ascending order and properly paginated.
- The page size should be defined by the caller of your API, but your API should not allow page sizes above 1000.
- Get a basket via its Id, which we prefer it to be a GUID.
- Get the 10 cheapest products from all the 10.000 products.


## Expectations

The following are our main expectations for the solution you provide:
- A solution designed to adhere to SOLID principles.
- Unit and end-to-end testing.
- Production ready code.
- Code submitted with multiple commits, ideally with a first commit once you start the Code Challenge.
- Comments on what improvement points you'd have.
- You must avoid frontend and/or end-user Authentication in your Basket API, also requesting an email address on any of your endpoints.
- We expect to be able to run your solution locally and run the tests you've implemented without any changes to your code.
- We don't expect you to store the baskets in any persistence layer, e.g. database or file. Storing it in memory is enough.




# Dev Notes

## Assumptions
- We don't need a front-end.
- We should get all products at once and work with them the Basket API instead of requesting them every time.
- Basket API doesn't require Authentication


## Using the API
- The swagger page for the Basket API shows all of the actions available to users of the API.


## Current issues
- I could have used Entity Framework Core with InMemory configuration but turns out I need to refresh my knowledge about Entity Configuration. I was afraid of spending too much time on its configuration and do not have time to do other features.
- HTTP client mock setup didn't work very well, had to ignore the test due to run out of time to investigate it.
- Just a few integration tests, didn't have time to create more scenarios


## What I would have done if I had more time
- In a real workd scenario application, I wouldn't have used ConcurrentDictionary as repository, I would have done using Entity Framework or even a Database (document or relational), depending on the requirements.
- Would have elaborated more the integration tests
- Would have created some acceptance tests
- Would have implemented some validations in the Create Basket flow, the way it is now, you can send any product and it will works. It was not clear now basket and product should work with each other in this application