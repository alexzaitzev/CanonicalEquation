# CanonicalEquation
Utility to canonizing equations

It works in two modes:
* Console
* From file

To use it from console, type
``` sh
CanonicalEquation.exe /m:console
```
or just
``` sh
CanonicalEquation.exe
```
To enter equations from the file, type
```sh
CanonicalEquation.exe /m:file YOUR_FILE_NAME
```
Result will be loaded in the same folder with .out extension

# Examples
```
CanonicalEquation.exe
Input equation
4.0xy = x^2 + x^2 - 8.0
- 2.0x^2 + 4.0xy + 8.0 = 0
Input equation
-(x -(x + y)) = 2.0x + y
- 2.0x = 0
```
