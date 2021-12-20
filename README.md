# CarFinder
Blazor Server app with backend api that shows a collection of cars. Car details can be shows by clicking on a car.
The car data is loaded into a grid. There is pagination and it is possible to search and sort the cars.

When no car data is loaded, i.e. at startup, a hangfire job is started that fetches the car data and stores it in cache.
Every ten minutes the cars are reloaded from the external api as to stay current. These minutes are configurable in the appsettings.json in the CarFinderApi project.

The car data is loaded from an external api. The url for this api is dynamic and expires after a day.
A new url must first be created and then set in the appsettings.json in the CarFinderApi project.
This can be done via this url:                https://carsapi1.docs.apiary.io/traffic
The api url will look something like this:    http://private-anon-051d191ef9-carsapi1.apiary-mock.com
