huggle
======

Huggle is an anti-vandalism tool for use on Wikipedia and other Wikimedia projects.

Download
=========
See https://en.wikipedia.org/wiki/Wikipedia:Huggle/Download

Repository rules
================

Before you make a commit to trunk make sure it works, please do not commit changes which are work in progress and not compileable, for that use own branch. If you see someone's change which doesn't work, comment it out and contact the person who's responsible for it, do this only in case that you can't compile or use program.

Use comments everywhere possible.

Standards
=========

All code should be compileable in visual c#, code is written in the philosophy that stability > performance > style.

Every function should have look similar to this:

```
/// <summary>
/// This is description what this function is doing
/// </summary>
/// <param name="parameter">This is a description of parameter</param>
/// <returns></returns>
string MyFunction(string parameter)
 {
   // what is it
   Core.History("Class.MyFunction()");
   ...
 }
```

Every class should look like:
 
```
class Class
{ //explanation
    public string blah; //variables
    // properties
    public string name
    {
      get 
      { 
        return "";
      } 
    } 
    public string foo() // functions
 }
}
```
