# About WeatherMap-api
 - This .NET 6 Core REST Web Api receives the city and country requests from WeatherMap Ui and requests for the weather objects from the OpenWeatherMap.org
 - Estimated time and effort spent on both Api and Ui deliverables was around 10-12 hours in total
 - .NET 6.0 Code Base
 - Swagger
 - CORS
 - Separate layers to facilitate API/ Core/ Infrastructure architecture
 - Dependency Injection
 - Rate Limiting **(a)**
 - CustomMiddleWares (Exception, RateLimit, x-Api-Key)
 - NLog (logs can be found here: ..\bin\Debug\net6.0\logs\WeatherMap-yyyy-MM-dd.log)
 - Exception Management to log and send all exceptions back to the Ui with StatusCode and proper message
 - Unit Tests **(b)**

## Installation (Recommend Visual Studio 2022 Professional)
Clone or download a zip from this repo to your preferred working directory.
Use the package manager to restore and install all NuGet Packages.

## Usage
Once the build is successful, launch the IIS Express to host this service, a SwaggerGen page should be available at https://localhost:2000/swagger/index.html

You can now use the WeatherMap-ui to check the weather in your city! Have fun!

## Contributing
Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

## Remarks
- (a) Currently the IP Rate Limit is implemented, I spent a significant effort working on rate-limit-by-key policy, but failed to deliver.
Instead, I implemented an alternate solution which is to track and manage the request attempts sent from the Ui. (See ..\Helper\apiKeys.txt and ..\Helper\attempts.txt for more info)

We may be able to refactor this feature by having more R&D on rate-limit-by-key policy or using the SessionState or Database to manage the attempts in the second development phase if desired.

- (b) Unit Tests started but I am not able to finish due to the sensitivity of timeframe. Can keep working on this if desired.

## Testing
- Start IIS Express to host the WeatherMap-api
  - Use Postman(or any equivalent) and setup a GET request, for security enhancement, the ApiKey is always sent in the Headers of https requests
  - URI: https://localhost:2000/api/WeatherForecast?country=uk&city=london
  - Headers: **Key** "ApiKey", **Value** "openweathermap_cf57ce9a-b9da-454f-8c0e-6f7527ba4370", click **Send**, expect the response body of the weather object
  - Headers: **Key** "ApiKey", **Value** "The-quick-brown-fox-jumps-over-the-lazy-dog", click **Send**, expect the response body of Unauthorized, check logs (see NLog above)
  - Headers: Uncheck **Key** "ApiKey", **Value** "openweathermap_cf57ce9a-b9da-454f-8c0e-6f7527ba4370", click **Send**, expect the response body of Unauthorized, check logs (see NLog above)
![image](https://github.com/jjrmie/WeatherMap-api/assets/139659998/848e216b-cbb5-42d6-9581-f93024a188fe)

 - The WeatherMap-api is responsible to construct and send the request to https://api.openweathermap.org/data/2.5/weather?q=London,uk&appid={API key} in order to get the weather object
 - Open another browser/ tab, navigate to WeatherMap-ui (http://localhost:4200)
 - Follow the Weathermap-ui README.md for more testing instructions
 - Modify the ApiKey.key1 in the environment.ts, select a country and a city while the api is running to test the Api Key authentication
 - Constantly monitoring ../WeatherMap-api/Helper/apiKeys.txt and ../WeatherMap-api/Helper/attempts.txt to learn more the behaviours of the api
 - Constantly monitoring ../bin/Debug/net6.0/logs/WeatherMap-yyyy-MM-dd.log to learn more the exceptions thrown by the api
 - **Remember to close those files opened for inspection/ monitoring before further testing or you would get IOException thrown**
