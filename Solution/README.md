# NOTES

## SynetecMvcAssessment.AcceptanceTests

### Setup
Place a chromedriver.exe in the project's root directory to run the tests on Chrome.

### Notes
The tests are tied to the data that is present in the initial database script. (i. e. if the data is changed, tests might fail.)


## SynetecMvcAssessment

### Notes
- I did not commit the change to the `web.config`, as only the connection string has changed. (And it contains sensitive data.)
- I've added a wrapper above the DbContext. This was necessary to have DI, but also it's generally good not to rely on something that is controlled (to some extent) by something outside of the app. (In this case the database schema.)
- A bit later I've realised that only one of the sets is being used in the app, so only that one needs to be exposed through the wrapper.
- I've added the Employee class in the Model so that the class' interface is locked down from our end. If anything changes in the schema or the Data classes, that change won't affect the app.
- Here I chose only to expose the properties that are actually needed (used) in the app. Less maintenance. Also more explicit domain model, but I'm aware that from a DDD perspective this may be a controversial choice.