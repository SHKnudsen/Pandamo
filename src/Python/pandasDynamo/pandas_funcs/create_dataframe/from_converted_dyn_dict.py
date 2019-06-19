import sys
import json
from ast import literal_eval
import pandas as pd
import re

# Create Dataframes
def from_converted_dyn_dict(data_dict):
    try:
        dataframe = pd.DataFrame(data_dict)
        return dataframe
    except Exception as e:
        return e