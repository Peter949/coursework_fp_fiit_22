import logging
import json
import random
import psycopg2
from telegram import Update, InlineKeyboardButton, InlineKeyboardMarkup # type: ignore
from telegram.ext import ApplicationBuilder, ContextTypes, CommandHandler, CallbackContext, CallbackQueryHandler # type: ignore


try:
    connection = psycopg2.connect(
        dbname='salon',
        user='postgres',
        password='pass',
        host='localhost',
        port='5432'
    )
except:
    print('Не удалось подключиться к бд')

cursor = connection.cursor()
cursor.execute('INSERT INTO users (name, age) VALUES (\'Максим\', 22)')
connection.commit()
cursor.execute('SELECT * FROM users')
users = cursor.fetchall()
cursor.close
connection.close()
print(users)
resources = {}


        

logging.basicConfig(
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s',
    level=logging.INFO
)


    



async def Start(update: Update, context: ContextTypes.DEFAULT_TYPE):

    #await context.bot.send_message(chat_id=update.effective_chat.id, 
    #                              text=random.choice(resources['Greetings']))
    buttons = [[InlineKeyboardButton("Нажми меня", callback_data="test")]]
    
    reply_markup = InlineKeyboardMarkup(buttons)
   
    # Отправляем сообщение с кнопкой
    await context.bot.send_message(
        chat_id=update.effective_chat.id,
        text="Привет! Это тест кнопок.",
        reply_markup=reply_markup
    )


async def End(update: Update, context: ContextTypes.DEFAULT_TYPE):
    await context.bot.send_message(chat_id=update.effective_chat.id, 
                                   text=random.choice(resources['Bye']))


async def Button_handler(update: Update, context: ContextTypes.DEFAULT_TYPE):
    query = update.callback_query
    await query.answer()
    await query.edit_message_text(text=users)

if __name__ == '__main__':
    with open ('resources.json', encoding='utf-8') as f:
        resources = json.load(f)
    #logging.getLogger().log(' ' , resources)
    #print(resources)

    token = open('token.txt', 'r', encoding='utf-8').read()
    application = ApplicationBuilder().token(token).build()

    start_handler = CommandHandler('start', Start)
    application.add_handler(start_handler)

    end_handler = CommandHandler('end', End)
    application.add_handler(end_handler)

    application.add_handler(CallbackQueryHandler(Button_handler))

    application.run_polling()














