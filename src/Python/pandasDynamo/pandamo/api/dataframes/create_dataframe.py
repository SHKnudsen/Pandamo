import json
import pandas as pd
import sys
from flask import Blueprint
from flask import current_app as app
from flask import request
from utillities.exceptions import ExceptionHelpers

mod = Blueprint('create_dataframe', __name__)
null = None
# Create Dataframes
@mod.route('by_dict/', methods=["POST"])
def by_dict():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.DataFrame(eval(jsonstr))
        df_json = df.to_json(orient='split')
        response = app.response_class(
            response=df_json,
            status=200,
            mimetype='application/json'
        )
    except:
        exception = ExceptionHelpers.format_exception(sys.exc_info())
        response = app.response_class(
            response=exception,
            status=400,
            mimetype='application/json'
        )
    return response

# Create Dataframes
@mod.route('by_excel/', methods=["POST"])
def by_excel():
    try:
        request_dict = request.get_json()
        file_path = request_dict['filePath']
        sheet_name = request_dict['sheetName']
        df = pd.read_excel(file_path, sheet_name=sheet_name, header=0, na_values=['', ' '])
        df_json = df.to_json(orient='split', date_format='iso')
        response = app.response_class(
                response=df_json,
                status=200,
                mimetype='application/json'
        )
    except:
        exception = ExceptionHelpers.format_exception(sys.exc_info())
        response = app.response_class(
            response=exception,
            status=400,
            mimetype='application/json'
        )
    return response