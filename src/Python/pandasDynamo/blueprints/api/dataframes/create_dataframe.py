import json
import pandas as pd
import sys
from flask import Blueprint
from flask import current_app as app
from flask import request

mod = Blueprint('create_dataframe', __name__)

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
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

# Create Dataframes
@mod.route('by_excel/', methods=["POST"])
def by_excel():
    request_dict = request.get_json()
    file_path = request_dict['filePath']
    sheet_name = request_dict['sheetName']
    df = pd.read_excel(file_path, sheet_name=sheet_name, header=0)
    df_json = df.to_json(orient='split')
    response = app.response_class(
            response=df_json,
            status=200,
            mimetype='application/json'
        )
    return response

@mod.route('test/', methods=["POST"])
def test():
    request_json = request.get_json()
    username = request_json['username']
    password = request_json['password']     
    print(username)
    print(password)
    response = app.response_class(
            response=request_json,
            status=200,
            mimetype='application/json'
        )
    return response