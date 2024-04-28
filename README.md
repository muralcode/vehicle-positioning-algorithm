# Vehicle-Positioning-Algorithm

## Note

A console program that can find the nearest vehicle position in the data binary file to each of the 10 co-ordinates provided. The program will do 10 lookups in less time than the standard benchmark. 
This benchmark is based on simply looping through each of the 2 million positions and keeping the closest to each given co-ordinate efficiently.
The app is implemented and not just parallelizing the brute force method explained above but doing a total of 20 million distance calculations.
## Local build process

It is recommended that the information in the other sections of this document are examined prior to attempting to compile and run the app. For the reasons discussed above, a developer cannot simply clone the repository and run the app.

### Software Prerequisites

In order to build the app you may use visual studio, the following software package is required:
- [Visual Studio](https://visualstudio.microsoft.com/)

### Procedure

1. Make sure that you have installed the above listed software on your machine.
2. Checkout the [develop](https://github.com/muralcode/vehicle-positioning-algorithm) branch
3. Make sure that your local **develop** branch has all the latest changes.
