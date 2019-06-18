import pandas as pd
from tabulate import tabulate

thisdict =	{
  "brand": ["Ford"]
}

df = pd.DataFrame(thisdict)

print(tabulate(df, headers='keys', tablefmt='orgtbl'))