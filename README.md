
<h1 style="text-align: center;">
 <img src="./Images/pandamo.png" alt="Pandamo Logo" width="300">
  <br>
  Pandamo
  <br>
</h1>

<h4 style="text-align: center;">Datascience for Dynamo</h4>


# Pandamo - pandas for Dynamo
Pandamo is a Dynamo package that utilizes the popular Python data science library Pandas. With Pandamo you can create and manipulate dataframes in Dynamo, just as you would do it when using pandas in Python.

# How it works
Out-of-the-box Dynamo only allows you to use IronPython, which dosen't include all the popular packages that makes Python so powerful. Pandamo is here to change that!

## The Tech
Pandamo has a few different pieces that needs to communicate with each other. On a high level Pandamo connects Dynamo with Python, to do this it uses a Flask server, a View Extension and Zero-Touch nodes.

The Pandamo View Extension is used to start the Flask server. The Pandamo server is a local server that only lives on your machine.

The Zero-Touch nodes are used to ask the Flask server to execute a specefic Python function with the inputs provided to the node.

The Flask server is the middle piece that makes Dynamo able to communicate with Python.
<div style="text-align:center"><img src="./Images/the_tech.png" /></div>

# How to install Pandamo
For now Pandmo lives right here in this repository on GitHub. In the future i will upload it to the Dynamo Package Manager. But for now you can find the newest release in the [`Releases`](https://www.google.com) section on this repository.

## Prerequisite
In order to make Pandamo work on your machine you need to install [`Miniconda for Python 3.7`](https://docs.conda.io/en/latest/miniconda.html). Miniconda is needed to create the Python environment that Pandamo uses.
<div style="text-align:center"><img src="./Images/miniconda_install.png" /></div> 
Miniconda is a lighter install of Anaconda, that wont install as many things. Pandamo is currently only tested with Miniconda so as of now it wont work if you use Anaconda. 

If you already have a version of Python 3.7 installed i would recomend uninstalling that and use the Miniconda installer so you only have one version of Python 3.7 on your machine. 

Make sure you install Miniconda to the default location C:\Users\\%USERPROFILE%\Miniconda3
