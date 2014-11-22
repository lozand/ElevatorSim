ElevatorSim
===========

<h2>Introduction</h2>
I got the idea of creating this repository from a question I asked myself (and later a few of my coworkers) about how an elevator functions and what rules it follows. It was a nice problem to talk about, but I wanted to implement this in a neat little program to test out the best way. The more I thought about it, the more interested I became, and I wanted to make this repository public so that I could get feedback in the process.

<h2>My Vision</h2>
The idea here is to create a series of algorithms for calling, sending and maintaining an elevator tower or series of towers and then being able to test those algorithms. This needs to be pretty robust and probably full of automated test cases. People ride these for crying out loud! Furthermore, I would like the "rules" of the elevator to be easily changed so that different rule sets can be tested for which is the most effective and why.
<br>
The output of this program will likely be in a text file (or potentially some other file type) so that it can be analyzed after the simulation has been completed. The main interface portion of this program should be in test cases, (which can be added to and altered as needed), but there should also be a user-interface option where a user can run the program and operate elevators that way.
<br>
At some point in the future, I would like to find the actual source code for elevators and compare my findings with that of actual functioning elevator code.

<h2>Current Solution</h2>
As is, the program begins and initializes the <code>Tower</code> class which kicks off a Task that constanstly runs and waits for <code>Calls</code>. For this project, a <code>Call</code> is when a button is pressed to call an elevator from <i>outside</i> the elevator while a <code>Command</code> is given <i>inside</i> an elevator. Therefore, when the program encounters a <code>Call</code> it is up to the <code>Tower</code> class to decide which elevator to send, but when a <code>Command</code> is given, it is up to that particular <code>Elevator</code> to decide when and where to go.
<br>
After the <code>Tower</code> is initialized, the <code>Elevator</code> is initialized where another task is kicked off that listens for <code>Commands</code> to be added to its <code>CommandQueue</code>.
The program currently feeds a <code>Command</code> to the <code>Tower</code> and the <code>Tower</code> sends an elevator to floor 5 and then floor 10. This trace is saved to a .txt file in the D: drive. 

No Test suite has been implemented at this time.
