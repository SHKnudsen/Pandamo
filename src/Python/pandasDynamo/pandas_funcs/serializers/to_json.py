import json
import pandas as pd

# Serialize dataframe
def dataframe_to_json(df):
    try:
        jsonstr = df.to_json()
        return jsonstr
    except Exception as e:
        return e