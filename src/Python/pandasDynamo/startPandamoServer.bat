@CALL "%UserProfile%\Miniconda3\condabin\conda.bat" activate pandamo
set FLASK_APP=run.py
set FLASK_ENV=development
flask run

