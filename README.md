
<h1 align="center">
 <img src="./Images/pandamo.png" alt="Pandamo Logo" width="300">
  <br>
  Pandamo
  <br>
</h1>

<h4 align="center">Datascience for Dynamo</h4>


# Pandamo - pandas for Dynamo
Pandamo is a Dynamo package that utilizes the popular Python data science library Pandas. With Pandamo you can create and manipulate dataframes in Dynamo, just as you would do it when using pandas in Python.

# How it works
Out-of-the-box Dynamo only allows you to use IronPython, which dosen't include all the popular packages that makes Python so powerful. Pandamo is here to change that!

## The Tech
Pandamo has a few different pieces that needs to communicate with each other. On a high level Pandamo connects Dynamo with Python, to do this it uses a Flask server, a View Extension and Zero-Touch nodes.

The Pandamo View Extension is used to start the Flask server. The Pandamo server is a local server that only lives on your machine.

The Zero-Touch nodes are used to ask the Flask server to execute a specefic Python function with the inputs provided to the node.

The Flask server is the middle piece that makes Dynamo able to communicate with Python.
<div align="center">
<img src="./Images/the_tech.png" width="500"/>
</div>

# How to install Pandamo
For now Pandmo lives right here in this repository on GitHub. In the future i will upload it to the Dynamo Package Manager. But for now you can find the newest release in the [`Releases`](https://github.com/SHKnudsen/Pandamo/releases) section on this repository.

## Prerequisite
In order to make Pandamo work on your machine you need to install [`Miniconda for Python 3.7`](https://docs.conda.io/en/latest/miniconda.html). Miniconda is needed to create the Python environment that Pandamo uses.
<div align="center"><img src="./Images/miniconda_install.png" /></div> 
Miniconda is a lighter install of Anaconda, that wont install as many things. Pandamo is currently only tested with Miniconda so as of now it wont work if you use Anaconda. 

If you already have a version of Python 3.7 installed i would recomend uninstalling that and use the Miniconda installer so you only have one version of Python 3.7 on your machine. 

Make sure you install Miniconda to the default location C:\Users\\%USERPROFILE%\Miniconda3

## Installing the package
To install the Pandamo package simply follow the steps below:
- download the `Pandamo` zip file from [`Releases`](https://github.com/SHKnudsen/Pandamo/releases) section on this repository.
- Extract the `DynamoPandas` folder to your Dynamo package folder (`C:\Users\\%USERPROFILE%\AppData\Roaming\Dynamo\Dynamo Core\2.x\packages`)

After this you should see a `Pandamo` button in your Dynamo ribbon. This is the Pandamo view extension.
<div align="center"><img src="./Images/dynamo_ribbon.png" /></div> 

# How to use Pandamo
The documentaion of how to use the package is still limited, this will be updated as soon as i have the time.

At the moment there are almost 50 nodes in the dyanmo package, none of them are documented at this point (i know.....).
<div align="center"><img src="./Images/all_nodes.png" /></div> 

Right now refer to the sample file `NaiveBayesPrediction.dyn` to see how some of the nodes work.

## Starting the Pandamo server
Before running any Pandamo nodes you need to start the Pandamo server, to do this open the Pandamo view extension and press the `Start Server` button and wait until you see the `Pandamo server is running locally on....`
<div align="center"><img src="./Images/start_pandamo_server.png" width="500"/></div> 

After this the Pandamo nodes are ready to use.

## Stopping the server
Pandamo automatically closes the server when you shut down Dynamo, however if you wnat to shut down the server without closing dynamo you can use the `Kill Server` button in the view extension window.

## Creating a DataFrame
There are 3 different ways to create a dataframe:

### By dictionary
<div align="center"><img src="./Images/dataframe_by_dictionary.png" /></div> 

### By columns and values
<div align="center"><img src="./Images/dataframe_by_columns_values.png" /></div> 

### From Excel
<div align="center"><img src="./Images/dataframe_from_excel.png" /></div> 

# More information
Stay tuned for more information about the nodes!

# Appendix
To learn more about Pandas and the different functions available in Pandamo take a look at the [`pandas documentation`](https://pandas.pydata.org/pandas-docs/stable/)

You can also find various cheat sheets for pandas which show some of the functionality. A good example is [`this`](https://github.com/pandas-dev/pandas/blob/master/doc/cheatsheet/Pandas_Cheat_Sheet.pdf)


