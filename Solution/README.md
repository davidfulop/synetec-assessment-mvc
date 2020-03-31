# NOTES

## SynetecMvcAssessment.AcceptanceTests

### Setup
Place a chromedriver.exe in the project's root directory to run the tests on Chrome. (Only tested on Chrome.)

### Notes
- The tests are tied to the data that is present in the initial database script. (i. e. if the data is changed, tests might fail.)
- I did put the Arrange and Act parts into Given_ and When_ methods, but I don't like to hide the asserts behind a Then_ method, because then I can't see what and how I'm actually asserting. If asked, I use Then_ methods too. :)


## SynetecMvcAssessment.UnitTests

### Notes
- Some tests are testing the same stuff as what acceptance tests test. I needed to add acceptance (black box) tests to be able to even touch the code.
Then, when I managed to split up the classes into a relatively sane structure, I needed to replicate some of the tests on a unit test level to be able to refactor without needing to wait for acc. tests to complete.
Some of the acceptance tests can go away now, but I wanted to keep them so you can see how I was progressing.


## SynetecMvcAssessment

### Notes
- I did not commit the change to the `web.config`, as only the connection string has changed. (And it contains sensitive data.)
- I've added a wrapper above the DbContext. This was necessary to have DI, but also it's generally good not to rely on something that is controlled (to some extent) by something outside of the app. (In this case the database schema.)
- A bit later I've realised that only one of the sets is being used in the app, so only that one needs to be exposed through the wrapper.
- I've added the Employee class in the Model so that the class' interface is locked down from our end. If anything changes in the schema or the Data classes, that change won't affect the app.
- Here I chose to expose only the properties that are actually needed (used) in the app. Less maintenance. Also more explicit domain model, but I'm aware that from a DDD perspective this may be a controversial choice.
- Not putting an interface around the two model classes was a conscious choice. They're just property bags. As long as they don't contain actual logic, I think it's pragmatic to just use the class, as in this case I'm not actually relying on any implementation.
- I deleted comments, as I think if the code and the tests aren't self-explanatory enough no comment will help. I left one comment though to explain a choice that may seem controversial in an interview. (Okay, one of many choices. :) )

The things above are not laws that I believe till the bitter end. This is just how I think based on my experiences so far. I'm aware that I'm not finished my learning, so I'm open to discuss these.

## Next Steps
- Making use of Async Linq methods in the service class. (It would require some tinkering.)
