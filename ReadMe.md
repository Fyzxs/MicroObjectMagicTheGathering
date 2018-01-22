# µObject Magic: The Gathering

This is an example project building an more complex and intelligent system demonstrating the cleanliness and maintainability of µObjects.

There are expected to be a number of major elements to the project:
* Library
* MtgLibrary
* Client
* Server

The Library is where core common code is. It's intended to be utilized and shared by the other 3 components.
The MtgLibrary is a MtG specific wrapper around the Library functionality. This is where MtG behavior is introduced.
The Server is the event bus. It just shuttles messages around.
The Client is whatever it needs to be. I expect the very initial will be heavily console based. This will allow quickly itterating and testing ideas to see what works well without the overhead of UI churn.

There'll be another component for debugging; I hope to get lots of debugging in here.
Not sure where it fits; but there will be a piece that moves through each phase of a turn and sends out messages for cards to kill themselves, clear events; untap. All those Phase change related events - that's what some piece will be responsible for.

