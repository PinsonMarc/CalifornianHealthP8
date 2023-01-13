# Canlifornian health documentation
Explaining the architecture decisions took in OpenClassroom's 8th projects.

## .net framework => .net 6

I have decided to use a newer version of .Net for this project to write a more modern application

## Docker and microservices

![architecture](https://github.com/PinsonMarc/CalifornianHealth/blob/master/documentation/Project%20architecture.png)

As the customer request to improve the website reliability I have decided to use **docker** and **3 microservices** 
#### Docker
Docker containers provide a level of isolation between different services, which means that a failure in one service will not affect the others. This improves the overall reliability and resilience of the system.
#### Microservices
Microservices can be scaled independently and managed independently, greatly improving maintainability and reducing dependencies. I have created 3 for this projects but the project is thought to easily include others:	
	-**californianhealthapp** is the web application
	-**booking-api** is an api accessible from `californianhealthapp` to manage appointments
	-**sqldata** contains the application database
To further understand, see the configurations at `docker-compose.yml` as well as in each dockerfiles

## New implementation

I changed the implementation of the booking system : 
	- The webapp 's `BookingController` now use a `BookingService` which access the `HTTPClient` to make the api calls
	- The Database have been seeded for production purpose with 3 consultants and a dozen appointments
	- The `booking-api`'s controller really contain the application and only him access the data context. It has been implemented against concurrency issues
	- Free appointment are those where the `PatientId` has not been defined

## Testing

The changes have been tested using at first **unit test** on the webapp 's `BookingController` behaviors by mocking the booking service. Moreover, a good amount of **integration tests** on the Booking API has been done using in memory database
#### Load testing
Load testing has been used to test the robustness of the project as well as to detect eventual concurrency issues
It has been done calling 6 request 500 times over a couple seconds. The full code report is available in the `loadTest` folder

If the only one request succeeded for the 5 appointment calls it is actually normal since the appointment has already been taken and the other calls return `409`
