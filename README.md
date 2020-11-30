# harel-project1
Store Web App on ASP.NET

# Requirements 

## functionality
* place orders to store locations for customers
* add a new customer
* search customers by name
* display details of an order
* display all order history of a store location
* display all order history of a customer
* client-side validation [new]
* server-side validation [new]
* exception handling
* CSRF prevention [new]
* persistent data; no prices, customers, order history, etc. hardcoded in C#
* logging of exceptions, EF SQL commands, and other events [newly required]
* (optional: order history can be sorted by earliest, latest, cheapest, most expensive)
* (optional: get a suggested order for a customer based on his order history)
* (optional: display some statistics based on order history)
* (optional: asynchronous network & file I/O)

## design
* use EF Core (either database-first approach with a SQL script or code-first approach with migrations) [changed]
* use an Azure SQL DB in third normal form
* don't use public fields
* define and use at least one interface

### core / domain / business logic
* class library
* contains all business logic
* contains domain classes (customer, order, store, product, etc.)
* documentation with `<summary>` XML comments on all public types and members (optional: `<params>` and `<return>`)
* (recommended: has no dependency on UI, data access, or any input/output considerations)

#### customer
* has first name, last name, etc.
* (optional: has a default store location to order from)

#### order
* has a store location
* has a customer
* has an order time (when the order was placed)
* can contain multiple kinds of product in the same order
* rejects orders with unreasonably high product quantities
* (optional: some additional business rules, like special deals)

#### location
* has an inventory
* inventory decreases when orders are accepted
* rejects orders that cannot be fulfilled with remaining inventory
* (optional: for at least one product, more than one inventory item decrements when ordering that product)

#### product (etc.)

### user interface
* ASP.NET Core MVC web application [new]
* separate request processing and presentation concerns with MVC pattern [new]
* strongly-typed views [new]
* minimize logic in views [new]
* use dependency injection [new]
* customize the default styling to some extent [new]
* keep CodeNamesLikeThis out of the visible UI [new]
* has only display- and input-related code

### data access
* class library
* contains EF Core DbContext and entity classes
* contains data access logic but no business logic
* use repository pattern for separation of concerns

### test
* at least 10 test methods
* focus on unit testing business logic
* data access tests (if present) should not impact the app's actual database [new]
