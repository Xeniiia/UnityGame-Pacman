# UnityGame-Pacman
Pacman game with client-server architecture.

All the logic of the game is processed on the server. Depending on the server messages, the client visualizes the player's position and level. The server processes the level data from the txt file at startup. Data transmission is based on sockets and asynchronous callbacks, messages are serialized using Protocol Buffers.
#
The assembled game is located in the Release folder. First, start the Server Release -> DemoPacman.
After that, run the UnityClientRelease client -> Pacman.

You're excellent.
