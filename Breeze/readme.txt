visual studio 2022, .NET 7.0


Assumptions
_______________________________________________________


Here is the revised version of the paragraph with corrected English:

- Since Y is an array of data, I assume that the calculations have to be performed for each sample of X, and the reduction is applied at the end.
- IDataAcquisitionSource assumes that the channel data has already been properly sampled. The sampled data is resampled to 100Hz.
- I assume that the functions have been properly tested before being added to the system.
- I assume that the provided parameters are runtime parameters and not constants; otherwise,
	I would have implemented the reading mechanism using repositories and performed the value replacement logic in the ExpressionTranslationProvider.GenerateExecutionAsync method.


Considerations
_______________________________________________________

Here is the revised version of the paragraph with corrected English:

- I changed 'Y, mX+c' to 'Y, m*X+c' because the math parser I ended up using, based on Infix, requires the arithmetic operator to be explicitly included.
- In a typical real-world implementation, both the runtime parameters and channel data would most likely be provided by the same telemetry data source.
	However, the exercise was provided with two separate files, making it a bit tricky to implement.
	It would be incorrect to hardcode the file names without adding an additional container layer, which is exactly what I did.
	The file names will remain static, but they will be contained within a zip file that represents the data for a given telemetry session.
	Telemetry systems often produce vast amounts of data, so the best approach is to compress the data after it has been analyzed for future reference.
- Since functions may change over time, it is important to keep track of the version of the functions used.
	However, this information is not currently being persisted, but it should be considered for future implementation.
- The requirements specified the use of files to store information. Although it would have been easier to store some of the data in SQLite or a similar database, I decided to adhere to the existing pattern.
- The telemetry data file (.tdf) is essentially a zip file that contains the provided files: channels.txt and parameters.txt.
- There are different ways to prepare the functions for execution. I have chosen a mechanism that expands the functions.
	This means that there may be additional loops during the translation time, but the cost of replacing segments of a string remains equally expensive.


Development strategy
_______________________________________________________

I am a big follower of TDD, specifically Outside-in, and have had the opportunity to work with Sandro Mancuso in the past.
I always strive to provide solutions that are clean, readable, easy to extend, maintain, and test.
However, sometimes convictions need to be set aside, and pragmatism becomes necessary, especially in the demanding F1 environment.
The fast-paced nature of the F1 industry, coupled with decreasing budget allowances, makes our job more challenging every day.
Occasionally, we are faced with the need to prioritize and make difficult decisions about what can be dropped.
While I make every effort to ensure that the code I produce is fully testable, achieving full coverage is not always feasible within delivery timelines.
While I understand that delivering a solution without tests is not ideal, what is even more challenging to accept is delivering code that cannot be easily tested or extended.
While I don't like making excuses for any shortcomings in my delivery, the decision to limit testing was made due to the system's extensive expansion.
However, I want to assure you that all the code has been designed to be testable and future-proofed.
During the development of the system I decided to set aside stubs of code which can be shared accross multiple projects. Generic adapters
were also added to this solution which I decided to call fabric. All the code in this solution is fully testable and by using the developed
adapters any client code can also be made testable.
I tried to keep the usage of external libraries to the minumum, the reason being, to keep the solution the the bare minumuns while showing
it's capabilities for extension.
In relation to the 'Channel Processing System', if I archieve my goal of building an application which is easier to read, than this description
becames less important. I tried to create an engine to process telemetry data which is easy to use and also to expand in the future.
Currently, it supports multiple channels as an input but for example in relation to metrics, it only support the 'mean' operation, but this is easily extended.
In terms of the testing strategy, I decided to use simple mocks instead of employing a mocking framework, just to keep things simpler.
There's definitely room for improvement but it was quite an interesting exercise.


Deliveries
_______________________________________________________

- There are two solutions: one contains the shared libraries (fabric), while the other is related to the implementation of the Channel Processing System.
- The shared libraries (fabric) are stored in the NugetStore. Nugets have already been included to eliminate the need for creating them.
- The Publish folder contains the executable for running the application without needing to open Visual Studio.
- The "Dywham.Breeze.Apps.ChannelProcessingSystem.Bootstrap.exe" file is used to run the application with files originally provided
	which where zip into Session 22-05-2023 091912111.tdf and are placed inside the folder: Resources\Unprocessed
- It is possible to run the application with two parameters if new calculations are to be performed without having to create a zip file i.e.:
	Dywham.Breeze.Apps.ChannelProcessingSystem.Bootstrap.exe "path../parameters.txt" channels.txt"
- For convenience, a file named 'Session 22-05-2023 091912111.tdf' containing the two provided files (channels.txt and parameters.txt) is processed by default.
	No argument needs to be provided to the executable - a shortcut "Apps.ChannelProcessingSystem" is provided at the root level


Support
_______________________________________________________

"I have done my best to provide a solution that is easier to understand and execute.
However, if the reviewer encounters any issues, I am available to help."










