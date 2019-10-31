from tabulate import tabulate as tb
import json
import sys
import pandas as pd
from flask import Blueprint
from flask import current_app as app
from flask import request
from utillities.exceptions import ExceptionHelpers

mod = Blueprint('format_dataframe', __name__)

@mod.route('tabulate/', methods=["POST"])
def tabulate():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        jsonstr = json.dumps(jsonstr)
        df = pd.read_json(eval(jsonstr), orient='split')
        headers = 'keys'
        tableformat = 'orgtbl'
        tabulated_df = tb(df, headers=headers, tablefmt=tableformat)
        response = app.response_class(
            response=tabulated_df,
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