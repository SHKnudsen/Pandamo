import json
import pandas as pd
import sys
from flask import Blueprint
from flask import current_app as app
from flask import request
from flask import jsonify
from scipy import stats
from utillities.exceptions import ExceptionHelpers


mod = Blueprint('stats', __name__)
null = None
@mod.route('z_score/', methods=["POST"])
def z_score():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        columns = request_dict['columns']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        if(columns is None):
            z_values = pd.np.abs(stats.zscore(df))
            z_values = pd.np.transpose(z_values)
            result = dict(zip(df.columns.values.tolist(), z_values.tolist())) 
        else:
            z_values = pd.np.abs(stats.zscore(df[columns]))
            z_values = pd.np.transpose(z_values)
            result = dict(zip(columns, z_values.tolist()))
            print(result)
        response = app.response_class(
            response=json.dumps(result),
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

@mod.route('drop_outliers/', methods=["POST"])
def drop_outliers():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        z_values = request_dict['z_values']
        standard_deviation = request_dict['standard_deviation']
        keys = list(z_values.keys())
        vals = [z_values[x] for x in keys]
        transpose_vals = pd.np.transpose(vals).tolist()
        
        bools = []
        for i in transpose_vals:
            b = all(item < standard_deviation for item in i)
            bools.append(b)
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        filtered_df = df[bools]
        df_json = filtered_df.to_json(orient='split', date_format='iso')
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
