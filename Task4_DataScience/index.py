# Import external modules
import pandas as pd
import numpy as np
# This is to ignore SettingWithCopyWarning in Pandas
pd.options.mode.chained_assignment = None


# Input : List of uniqe rents
# Dictionary : percentage of price w.r.t maximum rent of the room
def percentage_calculator(col):
    percent = {}
    prices = sorted(list(filter(lambda x: (x != 'Sold out') and (~np.isnan(x)), col)))
    for price in prices:
        percent[price] = round(((price / prices[-1])), 2)
    return percent


# Load spreadsheet
xl = pd.ExcelFile("HQplus_365_Day_7_Hotel_Export_BAR.xlsx")
# Load a sheet into a DataFrame by name: df1
df = xl.parse('Tabelle1')

# Assign percentage demand value to prices
for i in range(7):
    x = percentage_calculator(df['Hotel '+str(1+i)].unique().tolist())
    for j in range(len(df['Hotel '+str(1+i)])):
        if df['Hotel '+str(1+i)][j] == 'Sold out':
            df['Demand Hotel ' + str(1 + i)][j] = 1
        else:
            df['Demand Hotel '+str(1+i)][j]=x[df['Hotel '+str(1+i)][j]]

# Canculate average for each day
for i in range(len(df)):
    row = df.loc[i, ['Demand Hotel 1', 'Demand Hotel 2', 'Demand Hotel 3', 'Demand Hotel 4', 'Demand Hotel 5', 'Demand Hotel 6', 'Demand Hotel 7']]
    avg = round(sum(row)/7,2)
    df['Average Demand Hotel 1-7'][i] =avg

# Write file
df.to_excel('Results.xlsx',index=False)

