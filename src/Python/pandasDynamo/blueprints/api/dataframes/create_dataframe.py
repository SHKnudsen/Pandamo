import json
import pandas as pd
import sys
from flask import Blueprint

mod = Blueprint('create_dataframe', __name__)

# Create Dataframes
@mod.route('by_dict/<string:jsonstring>')
def by_dict(jsonstring):
    try:
        df = pd.DataFrame(eval(jsonstring))
        df_json = df.to_json(orient='index')
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
