# SharpAstrology.Base - The base library for SharpAstrology

This package provides enums and data types that are fundamental to all astrological systems. 
In addition, there are some utility functions that are often used in the SharpAstrology packages. 
The most important part in this module is the IEphemerides interface, 
which must be implemented before any other SharpAstrology package can be used.

## The IEphemerides interface
The IEphemerides interface is used for all astrological modules of SharpAstrology. 
SharpAstrology currently only provides one implementation of IEphemerides. 
This implementation uses SwissEphNet (binings of the swisseph C library). 
As swisseph has a dual license system, which is in the free version not compatible with SharpAstrology, 
the implementation is not an integral part of the base module. There may be other implementations of 
IEphemerides in the future.



