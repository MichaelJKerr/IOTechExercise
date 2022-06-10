# IOTechExercise
Unfortunately I was unable to test this solution in Linux as I have been having issues with my virtual machines, but the following set of commands should be able to run the solution.

First make "Program.cs" executable in it's properties, then cd into IOTechExercise-master.

sudo apt install mono-complete

mcs -out:Program.exe Program.cs

mono Program.exe

My apologies if this does not work.
