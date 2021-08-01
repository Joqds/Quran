import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:joqds_quran/bloc/bloc.dart';
import 'package:joqds_quran/ui/widgets/persian_text_input_formatter.dart';
import 'package:persian_number_utility/persian_number_utility.dart';

class QuranPageScreen extends StatefulWidget {
  const QuranPageScreen({Key? key}) : super(key: key);

  @override
  State<QuranPageScreen> createState() => _QuranPageScreenState();
}

class _QuranPageScreenState extends State<QuranPageScreen> {
  final TextEditingController textEditingController =
      TextEditingController(text: "1".toPersianDigit());

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(20.0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.center,
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          TextField(
            controller: textEditingController,
            keyboardType: const TextInputType.numberWithOptions(
                decimal: false, signed: false),
            textAlign: TextAlign.center,
            textInputAction: TextInputAction.done,
            onSubmitted: (value) {
              goToRead(context,
                  goToRead(context, int.parse(value.toEnglishDigit())));
            },
            inputFormatters: [
              PersianDigitOnlyTextInputFormatterWithRange(min: 0, max: 604)
            ],
            decoration: const InputDecoration(labelText: "شماره صفحه"),
          ),
          Padding(
            padding: const EdgeInsets.only(top: 10.0),
            child: ElevatedButton(
                onPressed: () => goToRead(context,
                    int.parse(textEditingController.text.toEnglishDigit())),
                child: const Text("برو به صفحه")),
          )
        ],
      ),
    );
  }

  goToRead(BuildContext context, int pageId) {
    BlocProvider.of<NavBloc>(context)
        .add(GoRead(type: ReadViewType.page, id: pageId, context: context));
  }
}
