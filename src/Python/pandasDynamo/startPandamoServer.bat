@CALL "%~dp0..\condabin\conda.bat" activate pandamo
cd C:\Users\SylvesterKnudsen\Documents\GitHub\Pandamo\src\Python\pandasDynamo
set FLASK_APP=run.py
set FLASK_ENV=development
flask run

