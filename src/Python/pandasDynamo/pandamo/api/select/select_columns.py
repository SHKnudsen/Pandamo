import json
import pandas as pd
import sys
from flask import Blueprint
from flask import current_app as app
from flask import request
from utillities.exceptions import ExceptionHelpers

mod = Blueprint('select_columns', __name__)
null = None

@mod.route('by_datatype/', methods=["POST"])
def by_datatype():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        include = request_dict['include']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        selected_columns = df.select_dtypes(include=include)
        df_json = selected_columns.to_json(orient='split', date_format='iso')
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