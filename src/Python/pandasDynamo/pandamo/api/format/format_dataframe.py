from tabulate import tabulate as tb
import json
import sys
import pandas as pd
from flask import Blueprint
from flask import current_app as app
from flask import request

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
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response