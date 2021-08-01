import 'package:flutter/material.dart';

class QuranPageScreen extends StatefulWidget {
  const QuranPageScreen({Key? key}) : super(key: key);

  @override
  State<QuranPageScreen> createState() => _QuranPageScreenState();
}

class _QuranPageScreenState extends State<QuranPageScreen> {
  final TextEditingController textEditingController = TextEditingController();

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
            keyboardType: TextInputType.number,
            decoration: const InputDecoration(labelText: "شماره صفحه"),
          ),
          Padding(
            padding: const EdgeInsets.only(top: 10.0),
            child: ElevatedButton(
                onPressed: () {}, child: const Text("برو به صفحه")),
          )
        ],
      ),
    );
  }
}
