# About WeatherMap-api
This .NET 6 Core REST Web Api receives the city and country requests from WeatherMap Ui and request for the weather object from the OpenWeatherMap.org
Estimated time and effort spent on this deliverable is around 10-12 hours in total.

# The WeatherMap-api is .NET 6.0 Code Base with the following features/ concepts in mind using Microsoft Visual Studio 2022 Professtional
 - Swagger
 - CORS
 - Separate layers to facilitate API/ Core/ Infrastructure architecture
 - Dependency Injection
 - Rate Limiting *
 - CustomMiddleWares (Exception, RateLimit, x-Api-key)
 - NLog (logs can be found here: ..\bin\Debug\net6.0\logs\)
 - Exception Management to log and send all exceptions back to the Ui with StatusCode and proper message
 - Unit Tests **

## Installation
Clone or download a zip from this repo to your preferred working directory
Use the package manager to restore and install NuGet Packages

## Usage
Once build successful, launch the IIS Express to host this service, a SwaggerGen page should be available at https://localhost:2000/swagger/index.html
Use the WeatherMap-ui to check the weather of your city! Have fun!

## Contributing
Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

## Remarks
- (*) Currently the IP Rate Limit is implemented, I spent a significant effot working on rate-limit-by-key policy, but failed to deliver.
Instead, I implemented an alternate solution which is to track and manage the request attempts sent from the Ui. (See ..\Helper\apiKeys.txt and ..\Helper\attempts.txt for more info)
We may be able to improve this feature by using the SessionState or database in the second phase.

- (**) Unit Tests started but I am not able to finish due to the sensitivity of timeframe. Can keep working on this if desired.
