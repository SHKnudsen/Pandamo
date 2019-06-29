from tabulate import tabulate as tb
import json
import sys
import pandas as pd
from flask import Blueprint
from flask import current_app as app

mod = Blueprint('format_dataframe', __name__)

@mod.route('tabulate/<string:jsonstr>')
def tabulate(jsonstr):
    try:
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='index')
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